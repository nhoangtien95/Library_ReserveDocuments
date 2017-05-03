namespace ThuVien.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTableHK1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HK",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Hocky = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.HK");
        }
    }
}
