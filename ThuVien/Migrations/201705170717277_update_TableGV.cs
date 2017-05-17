namespace ThuVien.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_TableGV : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GiangVien", "Tab", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GiangVien", "Tab");
        }
    }
}
