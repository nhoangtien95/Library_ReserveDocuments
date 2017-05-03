namespace ThuVien.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateID_GV_MH : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.MonHoc");
            AddColumn("dbo.GiangVien", "MonHocId", c => c.Int(nullable: false));
            AddColumn("dbo.GiangVien", "BookId", c => c.Int(nullable: false));
            AddColumn("dbo.GiangVien", "PaperId", c => c.Int(nullable: false));
            AddColumn("dbo.GiangVien", "OtherId", c => c.Int(nullable: false));
            AddColumn("dbo.MonHoc", "ID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.MonHoc", "CourseID", c => c.String(nullable: false));
            AlterColumn("dbo.TaiLieu", "LoaiTaiLieu", c => c.String());
            AddPrimaryKey("dbo.MonHoc", "ID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.MonHoc");
            AlterColumn("dbo.TaiLieu", "LoaiTaiLieu", c => c.String(nullable: false));
            AlterColumn("dbo.MonHoc", "CourseID", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.MonHoc", "ID");
            DropColumn("dbo.GiangVien", "OtherId");
            DropColumn("dbo.GiangVien", "PaperId");
            DropColumn("dbo.GiangVien", "BookId");
            DropColumn("dbo.GiangVien", "MonHocId");
            AddPrimaryKey("dbo.MonHoc", "CourseID");
        }
    }
}
