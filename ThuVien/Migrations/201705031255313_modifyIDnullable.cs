namespace ThuVien.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyIDnullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.GiangVien", "BookId", c => c.Int());
            AlterColumn("dbo.GiangVien", "PaperId", c => c.Int());
            AlterColumn("dbo.GiangVien", "OtherId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.GiangVien", "OtherId", c => c.Int(nullable: false));
            AlterColumn("dbo.GiangVien", "PaperId", c => c.Int(nullable: false));
            AlterColumn("dbo.GiangVien", "BookId", c => c.Int(nullable: false));
        }
    }
}
