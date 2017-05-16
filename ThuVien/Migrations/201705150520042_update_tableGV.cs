namespace ThuVien.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_tableGV : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GiangVien", "Status", c => c.Int(nullable: false));
            AddColumn("dbo.GiangVien", "Note", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GiangVien", "Note");
            DropColumn("dbo.GiangVien", "Status");
        }
    }
}
