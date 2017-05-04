namespace ThuVien.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateRela : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TL_BaiBao", "TaiLieu_ID", "dbo.TaiLieu");
            DropForeignKey("dbo.TL_Khac", "TaiLieu_ID", "dbo.TaiLieu");
            DropForeignKey("dbo.TL_Sach", "TaiLieu_ID", "dbo.TaiLieu");
            DropIndex("dbo.TL_BaiBao", new[] { "TaiLieu_ID" });
            DropIndex("dbo.TL_Khac", new[] { "TaiLieu_ID" });
            DropIndex("dbo.TL_Sach", new[] { "TaiLieu_ID" });
            AlterColumn("dbo.TL_BaiBao", "MucDich", c => c.String());
            AlterColumn("dbo.TL_Khac", "MucDich", c => c.String());
            AlterColumn("dbo.TL_Sach", "MucDich", c => c.String());
            DropColumn("dbo.TL_BaiBao", "TaiLieu_ID");
            DropColumn("dbo.TL_Khac", "TaiLieu_ID");
            DropColumn("dbo.TL_Sach", "TaiLieu_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TL_Sach", "TaiLieu_ID", c => c.Int());
            AddColumn("dbo.TL_Khac", "TaiLieu_ID", c => c.Int());
            AddColumn("dbo.TL_BaiBao", "TaiLieu_ID", c => c.Int());
            AlterColumn("dbo.TL_Sach", "MucDich", c => c.String(nullable: false));
            AlterColumn("dbo.TL_Khac", "MucDich", c => c.String(nullable: false));
            AlterColumn("dbo.TL_BaiBao", "MucDich", c => c.String(nullable: false));
            CreateIndex("dbo.TL_Sach", "TaiLieu_ID");
            CreateIndex("dbo.TL_Khac", "TaiLieu_ID");
            CreateIndex("dbo.TL_BaiBao", "TaiLieu_ID");
            AddForeignKey("dbo.TL_Sach", "TaiLieu_ID", "dbo.TaiLieu", "ID");
            AddForeignKey("dbo.TL_Khac", "TaiLieu_ID", "dbo.TaiLieu", "ID");
            AddForeignKey("dbo.TL_BaiBao", "TaiLieu_ID", "dbo.TaiLieu", "ID");
        }
    }
}
