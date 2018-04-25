using System.Globalization;

namespace Gma.CodeCloud.Controls.TextAnalyses.Stemmers.En
{
    //   Porter stemmer in CSharp, based on the C port. The original paper is in
    //       Porter, 1980, An algorithm for suffix stripping, Program, Vol. 14,
    //       no. 3, pp 130-137,
    //   See also http://www.tartarus.org/~martin/PorterStemmer

    /// <summary>
    ///     The Stemmer class transforms a word into its root form.
    /// </summary>
    public struct PorterStem
    {
        private int _mCurrentIndex;
        private int _mEndIndex;
        private readonly char[] _mTerm;
        private bool _mIsReduced;

        public PorterStem(string term)
            : this()
        {
            var enUs = CultureInfo.GetCultureInfo(1033);
            _mTerm =
                term
                    .ToLower(enUs)
                    .ToCharArray();

            _mEndIndex = _mTerm.Length - 1;
        }

        public override string ToString()
        {
            ReduceToStem();
            var length = _mEndIndex + 1;
            return new string(_mTerm, 0, length);
        }

        public void ReduceToStem()
        {
            if (_mEndIndex <= 1 || _mIsReduced) return;

            TrimCommonEndings();
            TurnTerminalY2I();
            DoubleDuffices2Singles();
            HandleFullNess();
            HandleAntEnce();
            RemoveFinalE();
            _mIsReduced = true;
        }

        private static bool IsConsonantAt(char[] array, int index)
        {
            switch (array[index])
            {
                case 'a':
                case 'e':
                case 'i':
                case 'o':
                case 'u':
                    return false;
                case 'y':
                    return index == 0 ? true : !IsConsonantAt(array, index - 1);
                default:
                    return true;
            }
        }

        /* m() measures the number of consonant sequences between 0 and currentIndex. if c is
           a consonant sequence and v a vowel sequence, and <..> indicates arbitrary
           presence,

              <c><v>       gives 0
              <c>vc<v>     gives 1
              <c>vcvc<v>   gives 2
              <c>vcvcvc<v> gives 3
              ....
        */
        private static int CountConsonantsInSequence(char[] array, int start, int end)
        {
            var counter = 0;
            var index = start;
            while (true)
            {
                if (index > end) return counter;
                if (!IsConsonantAt(array, index)) break;
                index++;
            }
            index++;
            while (true)
            {
                while (true)
                {
                    if (index > end) return counter;
                    if (IsConsonantAt(array, index)) break;
                    index++;
                }
                index++;
                counter++;
                while (true)
                {
                    if (index > end) return counter;
                    if (!IsConsonantAt(array, index)) break;
                    index++;
                }
                index++;
            }
        }

        /* vowelinstem() is true <=> 0,...currentIndex contains a vowel */
        private static bool ContainsVowel(char[] term, int start, int end)
        {
            int index;
            for (index = start; index <= end; index++)
                if (!IsConsonantAt(term, index))
                    return true;
            return false;
        }

        private static bool ContainsConsonant(char[] term, int start, int end)
        {
            int index;
            for (index = start; index <= end; index++)
                if (IsConsonantAt(term, index))
                    return true;
            return false;
        }

        /* doublec(index) is true <=> index,(currentIndex-1) contain a double consonant. */
        private static bool IsDoubleConsonantAt(char[] term, int index)
        {
            if (index < 1)
                return false;

            if (term[index] != term[index - 1])
                return false;
            return IsConsonantAt(term, index);
        }

        /* cvc(index) is true <=> index-2,index-1,index has the form consonant - vowel - consonant
           and also if the second c is not w,x or y. this is used when trying to
           restore an e at the end of a short word. e.g.

              cav(e), lov(e), hop(e), crim(e), but
              snow, box, tray.

        */
        private static bool IsConsonantVowelConsonantAt(char[] term, int index)
        {
            if (index < 2 || !IsConsonantAt(term, index) || IsConsonantAt(term, index - 1) ||
                !IsConsonantAt(term, index - 2))
                return false;
            int ch = term[index];
            if (ch == 'w' || ch == 'x' || ch == 'y')
                return false;
            return true;
        }

        private bool TrimIfEndsWith(string s)
        {
            if (EndsWith(_mTerm, _mEndIndex, s))
            {
                _mCurrentIndex = _mEndIndex - s.Length;
                return true;
            }
            return false;
        }

        private static bool EndsWith(char[] term, int end, string ending)
        {
            var startOffset = end - ending.Length + 1;
            if (startOffset < 0)
                return false;
            for (var index = 0; index < ending.Length; index++)
                if (term[startOffset + index] != ending[index])
                    return false;
            return true;
        }

        private static bool ReplaceEnding(char[] term, string originalEnding, string newEnding, ref int end)
        {
            if (!EndsWith(term, end, originalEnding))
                return false;

            var length = newEnding.Length;
            var startOffest = end - originalEnding.Length + 1;
            for (var index = 0; index < length; index++)
                term[startOffest + index] = newEnding[index];
            end = end - originalEnding.Length + newEnding.Length;
            return true;
        }

        /* setto(s) sets (currentIndex+1),...endIndex to the characters in the string s, readjusting endIndex. */
        private void SetEndingTo(string s)
        {
            var l = s.Length;
            var o = _mCurrentIndex + 1;
            var sc = s.ToCharArray();
            for (var index = 0; index < l; index++)
                _mTerm[o + index] = sc[index];
            _mEndIndex = _mCurrentIndex + l;
        }

        /* r(s) is used further down. */
        private void SetEndingIfContainsConsonants(string s)
        {
            if (ContainsConsonant(_mTerm, 0, _mCurrentIndex))
                SetEndingTo(s);
        }

        /* step1() gets rid of plurals and -ed or -ing. e.g.
               caresses  ->  caress
               ponies    ->  poni
               ties      ->  ti
               caress    ->  caress
               cats      ->  cat

               feed      ->  feed
               agreed    ->  agree
               disabled  ->  disable

               matting   ->  mat
               mating    ->  mate
               meeting   ->  meet
               milling   ->  mill
               messing   ->  mess

               meetings  ->  meet

        */

        private void TrimCommonEndings()
        {
            if (_mTerm[_mEndIndex] == 's')
                if (ReplaceEnding(_mTerm, "sses", "ss", ref _mEndIndex))
                {
                    //m_EndIndex -= 2;
                }
                else if (ReplaceEnding(_mTerm, "ies", "i", ref _mEndIndex))
                {
                    //Setto("i");
                }
                else if (_mTerm[_mEndIndex - 1] != 's')
                {
                    ReplaceEnding(_mTerm, "s", string.Empty, ref _mEndIndex);
                }

            if (TrimIfEndsWith("eed"))
            {
                if (CountConsonantsInSequence(_mTerm, 0, _mCurrentIndex) > 0)
                    _mEndIndex--;
            }
            else if ((TrimIfEndsWith("ed") || TrimIfEndsWith("ing")) && ContainsVowel(_mTerm, 0, _mCurrentIndex))
            {
                _mEndIndex = _mCurrentIndex;
                if (TrimIfEndsWith("at"))
                {
                    SetEndingTo("ate");
                }
                else if (TrimIfEndsWith("bl"))
                {
                    SetEndingTo("ble");
                }
                else if (TrimIfEndsWith("iz"))
                {
                    SetEndingTo("ize");
                }
                else if (IsDoubleConsonantAt(_mTerm, _mEndIndex))
                {
                    _mEndIndex--;
                    int ch = _mTerm[_mEndIndex];
                    if (ch == 'l' || ch == 's' || ch == 'z')
                        _mEndIndex++;
                }
                else if (CountConsonantsInSequence(_mTerm, 0, _mCurrentIndex) == 1 &&
                         IsConsonantVowelConsonantAt(_mTerm, _mEndIndex))
                {
                    SetEndingTo("e");
                }
            }
        }

        /* step2() turns terminal y to i when there is another vowel in the stem. */
        private void TurnTerminalY2I()
        {
            if (TrimIfEndsWith("y") && ContainsVowel(_mTerm, 0, _mCurrentIndex))
                _mTerm[_mEndIndex] = 'i';
        }

        /* step3() maps double suffices to single ones. so -ization ( = -ize plus
           -ation) maps to -ize etc. note that the string before the suffix must give
           m() > 0. */
        private void DoubleDuffices2Singles()
        {
            if (_mEndIndex == 0)
                return;

            switch (_mTerm[_mEndIndex - 1])
            {
                case 'a':
                    if (TrimIfEndsWith("ational"))
                    {
                        SetEndingIfContainsConsonants("ate");
                        break;
                    }
                    if (TrimIfEndsWith("tional"))
                        SetEndingIfContainsConsonants("tion");
                    break;
                case 'c':
                    if (TrimIfEndsWith("enci"))
                    {
                        SetEndingIfContainsConsonants("ence");
                        break;
                    }
                    if (TrimIfEndsWith("anci"))
                        SetEndingIfContainsConsonants("ance");
                    break;
                case 'e':
                    if (TrimIfEndsWith("izer"))
                        SetEndingIfContainsConsonants("ize");
                    break;
                case 'l':
                    if (TrimIfEndsWith("bli"))
                    {
                        SetEndingIfContainsConsonants("ble");
                        break;
                    }
                    if (TrimIfEndsWith("alli"))
                    {
                        SetEndingIfContainsConsonants("al");
                        break;
                    }
                    if (TrimIfEndsWith("entli"))
                    {
                        SetEndingIfContainsConsonants("ent");
                        break;
                    }
                    if (TrimIfEndsWith("eli"))
                    {
                        SetEndingIfContainsConsonants("e");
                        break;
                    }
                    if (TrimIfEndsWith("ousli"))
                        SetEndingIfContainsConsonants("ous");
                    break;
                case 'o':
                    if (TrimIfEndsWith("ization"))
                    {
                        SetEndingIfContainsConsonants("ize");
                        break;
                    }
                    if (TrimIfEndsWith("ation"))
                    {
                        SetEndingIfContainsConsonants("ate");
                        break;
                    }
                    if (TrimIfEndsWith("ator"))
                        SetEndingIfContainsConsonants("ate");
                    break;
                case 's':
                    if (TrimIfEndsWith("alism"))
                    {
                        SetEndingIfContainsConsonants("al");
                        break;
                    }
                    if (TrimIfEndsWith("iveness"))
                    {
                        SetEndingIfContainsConsonants("ive");
                        break;
                    }
                    if (TrimIfEndsWith("fulness"))
                    {
                        SetEndingIfContainsConsonants("ful");
                        break;
                    }
                    if (TrimIfEndsWith("ousness"))
                        SetEndingIfContainsConsonants("ous");
                    break;
                case 't':
                    if (TrimIfEndsWith("aliti"))
                    {
                        SetEndingIfContainsConsonants("al");
                        break;
                    }
                    if (TrimIfEndsWith("iviti"))
                    {
                        SetEndingIfContainsConsonants("ive");
                        break;
                    }
                    if (TrimIfEndsWith("biliti"))
                        SetEndingIfContainsConsonants("ble");
                    break;
                case 'g':
                    if (TrimIfEndsWith("logi"))
                        SetEndingIfContainsConsonants("log");
                    break;
                default:
                    break;
            }
        }

        /* step4() deals with -ic-, -full, -ness etc. similar strategy to step3. */
        private void HandleFullNess()
        {
            switch (_mTerm[_mEndIndex])
            {
                case 'e':
                    if (TrimIfEndsWith("icate"))
                    {
                        SetEndingIfContainsConsonants("ic");
                        break;
                    }
                    if (TrimIfEndsWith("ative"))
                    {
                        SetEndingIfContainsConsonants("");
                        break;
                    }
                    if (TrimIfEndsWith("alize"))
                    {
                        SetEndingIfContainsConsonants("al");
                    }
                    break;
                case 'i':
                    if (TrimIfEndsWith("iciti"))
                        SetEndingIfContainsConsonants("ic");
                    break;
                case 'l':
                    if (TrimIfEndsWith("ical"))
                    {
                        SetEndingIfContainsConsonants("ic");
                        break;
                    }
                    if (TrimIfEndsWith("ful"))
                        SetEndingIfContainsConsonants("");
                    break;
                case 's':
                    if (TrimIfEndsWith("ness"))
                        SetEndingIfContainsConsonants("");
                    break;
            }
        }

        /* step5() takes off -ant, -ence etc., in context <c>vcvc<v>. */
        private void HandleAntEnce()
        {
            if (_mEndIndex == 0)
                return;

            switch (_mTerm[_mEndIndex - 1])
            {
                case 'a':
                    if (TrimIfEndsWith("al")) break;
                    return;
                case 'c':
                    if (TrimIfEndsWith("ance")) break;
                    if (TrimIfEndsWith("ence")) break;
                    return;
                case 'e':
                    if (TrimIfEndsWith("er")) break;
                    return;
                case 'i':
                    if (TrimIfEndsWith("ic")) break;
                    return;
                case 'l':
                    if (TrimIfEndsWith("able")) break;
                    if (TrimIfEndsWith("ible")) break;
                    return;
                case 'n':
                    if (TrimIfEndsWith("ant")) break;
                    if (TrimIfEndsWith("ement")) break;
                    if (TrimIfEndsWith("ment")) break;
                    /* element etc. not stripped before the m */
                    if (TrimIfEndsWith("ent")) break;
                    return;
                case 'o':
                    if (TrimIfEndsWith("ion") && _mCurrentIndex >= 0 &&
                        (_mTerm[_mCurrentIndex] == 's' || _mTerm[_mCurrentIndex] == 't')) break;
                    /* currentIndex >= 0 fixes Bug 2 */
                    if (TrimIfEndsWith("ou")) break;
                    return;
                /* takes care of -ous */
                case 's':
                    if (TrimIfEndsWith("ism")) break;
                    return;
                case 't':
                    if (TrimIfEndsWith("ate")) break;
                    if (TrimIfEndsWith("iti")) break;
                    return;
                case 'u':
                    if (TrimIfEndsWith("ous")) break;
                    return;
                case 'v':
                    if (TrimIfEndsWith("ive")) break;
                    return;
                case 'z':
                    if (TrimIfEndsWith("ize")) break;
                    return;
                default:
                    return;
            }
            if (CountConsonantsInSequence(_mTerm, 0, _mCurrentIndex) > 1)
                _mEndIndex = _mCurrentIndex;
        }

        /* step6() removes a final -e if m() > 1. */
        private void RemoveFinalE()
        {
            _mCurrentIndex = _mEndIndex;

            if (_mTerm[_mEndIndex] == 'e')
            {
                var a = CountConsonantsInSequence(_mTerm, 0, _mCurrentIndex);
                if (a > 1 || a == 1 && !IsConsonantVowelConsonantAt(_mTerm, _mEndIndex - 1))
                    _mEndIndex--;
            }
            if (_mTerm[_mEndIndex] == 'l' && IsDoubleConsonantAt(_mTerm, _mEndIndex) &&
                CountConsonantsInSequence(_mTerm, 0, _mCurrentIndex) > 1)
                _mEndIndex--;
        }
    }
}