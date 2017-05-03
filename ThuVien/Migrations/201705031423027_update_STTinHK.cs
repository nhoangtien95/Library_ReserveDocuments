namespace ThuVien.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_STTinHK : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HK", "STT", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HK", "STT");
        }
    }
}
