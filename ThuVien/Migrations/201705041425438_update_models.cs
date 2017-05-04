namespace ThuVien.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_models : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TL_BaiBao", "Loai", c => c.String());
            AddColumn("dbo.TL_Khac", "Loai", c => c.String());
            AddColumn("dbo.TL_Sach", "Loai", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TL_Sach", "Loai");
            DropColumn("dbo.TL_Khac", "Loai");
            DropColumn("dbo.TL_BaiBao", "Loai");
        }
    }
}
