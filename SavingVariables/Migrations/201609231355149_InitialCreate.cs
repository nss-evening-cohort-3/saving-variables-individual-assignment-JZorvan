namespace SavingVariables.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Variables",
                c => new
                    {
                        VarId = c.Int(nullable: false, identity: true),
                        VarLetter = c.String(nullable: false, maxLength: 1),
                        VarValue = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VarId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Variables");
        }
    }
}
