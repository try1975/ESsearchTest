namespace Price.Db.Postgress.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V06 : DbMigration
    {
        public override void Up()
        {
            AddColumn("public.SearchItems", "ElasticCount", c => c.Int(nullable:false, defaultValue:0));
            Sql("update public.\"SearchItems\" s set \"ElasticCount\"  = (select count(*) from public.\"Contents\" c where c.\"SearchItemId\"=s.\"Id\")");
            Sql("CREATE FUNCTION public.increment_elastic_count()\r\n    RETURNS trigger\r\n    LANGUAGE \'plpgsql\'\r\n    COST 100\r\n    VOLATILE NOT LEAKPROOF \r\nAS $BODY$\r\nBEGIN\r\nUPDATE public.\"SearchItems\"\r\n SET \"ElasticCount\" = \"ElasticCount\" + 1\r\n  WHERE \"Id\" = NEW.\"SearchItemId\";\r\n  RETURN NULL;\r\n END\r\n\t$BODY$;");
            Sql("ALTER FUNCTION public.increment_elastic_count()\r\n OWNER TO postgres;");
            Sql("CREATE FUNCTION public.decrement_elastic_count()\r\n    RETURNS trigger\r\n    LANGUAGE \'plpgsql\'\r\n    COST 100\r\n    VOLATILE NOT LEAKPROOF \r\nAS $BODY$\r\nBEGIN\r\nUPDATE public.\"SearchItems\"\r\n SET \"ElasticCount\" = \"ElasticCount\" - 1\r\n  WHERE \"Id\" = OLD.\"SearchItemId\";\r\n  RETURN NULL;\r\n END\r\n\t$BODY$;");
            Sql("ALTER FUNCTION public.decrement_elastic_count()\r\n OWNER TO postgres;");
            Sql("CREATE TRIGGER elastic_count_insert_tr\r\n    AFTER INSERT\r\n    ON public.\"Contents\"\r\n    FOR EACH ROW\r\n    EXECUTE PROCEDURE public.increment_elastic_count();");
            Sql("CREATE TRIGGER elastic_count_delete_tr\r\n    BEFORE DELETE\r\n    ON public.\"Contents\"\r\n    FOR EACH ROW\r\n    EXECUTE PROCEDURE public.decrement_elastic_count();");
            Sql("CREATE TRIGGER elastic_count_update_tr_01\r\n    AFTER UPDATE\r\n    ON public.\"Contents\"\r\n FOR EACH ROW EXECUTE PROCEDURE public.decrement_elastic_count();");
            Sql("CREATE TRIGGER elastic_count_update_tr_02\r\n    AFTER UPDATE\r\n    ON public.\"Contents\"\r\n FOR EACH ROW EXECUTE PROCEDURE public.increment_elastic_count();");
            
            AddColumn("public.SearchItems", "InternetCount", c => c.Int(nullable:false, defaultValue:0));
            Sql("update public.\"SearchItems\" s set \"InternetCount\"  = (select count(*) from public.history c where c.session_id=s.\"InternetSessionId\")");
            Sql("CREATE FUNCTION public.increment_internet_count()\r\n    RETURNS trigger\r\n    LANGUAGE \'plpgsql\'\r\n    COST 100\r\n    VOLATILE NOT LEAKPROOF \r\nAS $BODY$\r\nBEGIN\r\nUPDATE public.\"SearchItems\"\r\n SET \"InternetCount\" = \"InternetCount\" + 1\r\n  WHERE \"InternetSessionId\" = NEW.session_id;\r\n  RETURN NULL;\r\n END\r\n\t$BODY$;");
            Sql("ALTER FUNCTION public.increment_internet_count()\r\n OWNER TO postgres;");
            Sql("CREATE FUNCTION public.decrement_internet_count()\r\n    RETURNS trigger\r\n    LANGUAGE \'plpgsql\'\r\n    COST 100\r\n    VOLATILE NOT LEAKPROOF \r\nAS $BODY$\r\nBEGIN\r\nUPDATE public.\"SearchItems\"\r\n SET \"InternetCount\" = \"InternetCount\" - 1\r\n  WHERE \"InternetSessionId\" = OLD.session_id;\r\n  RETURN NULL;\r\n END\r\n\t$BODY$;");
            Sql("ALTER FUNCTION public.decrement_internet_count()\r\n OWNER TO postgres;");
            Sql("CREATE TRIGGER internet_count_insert_tr\r\n    AFTER INSERT\r\n    ON public.history\r\n    FOR EACH ROW\r\n    EXECUTE PROCEDURE public.increment_internet_count();");
            Sql("CREATE TRIGGER internet_count_delete_tr\r\n    BEFORE DELETE\r\n    ON public.history\r\n    FOR EACH ROW\r\n    EXECUTE PROCEDURE public.decrement_internet_count();");
            Sql("CREATE TRIGGER internet_count_update_tr_01\r\n    AFTER UPDATE\r\n    ON public.history\r\n    FOR EACH ROW EXECUTE PROCEDURE public.decrement_internet_count();");
            Sql("CREATE TRIGGER internet_count_update_tr_02\r\n    AFTER UPDATE\r\n    ON public.history\r\n    FOR EACH ROW EXECUTE PROCEDURE public.increment_internet_count();");
        }
        
        public override void Down()
        {
            Sql("DROP TRIGGER elastic_count_update_tr_02 ON public.\"Contents\";");
            Sql("DROP TRIGGER elastic_count_update_tr_01 ON public.\"Contents\";");
            Sql("DROP TRIGGER elastic_count_delete_tr ON public.\"Contents\";");
            Sql("DROP TRIGGER elastic_count_insert_tr ON public.\"Contents\";");
            Sql("DROP FUNCTION public.decrement_elastic_count();");
            Sql("DROP FUNCTION public.increment_elastic_count();");
            DropColumn("public.SearchItems", "ElasticCount");
            
            Sql("DROP TRIGGER internet_count_update_tr_02 ON public.history;");
            Sql("DROP TRIGGER internet_count_update_tr_01 ON public.history;");
            Sql("DROP TRIGGER internet_count_delete_tr ON public.history;");
            Sql("DROP TRIGGER internet_count_insert_tr ON public.history;");
            Sql("DROP FUNCTION public.decrement_internet_count();");
            Sql("DROP FUNCTION public.increment_internet_count();");
            DropColumn("public.SearchItems", "InternetCount");
        }
    }
}