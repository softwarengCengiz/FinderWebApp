using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Migrations
{
    public partial class _20240602163925_trigger_added_to_events : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE TRIGGER [FinderWebAppDB].[dbo].[trg_UpdatePollingsIsActive]
                ON [FinderWebAppDB].[dbo].[Events]
                AFTER UPDATE
                AS
                BEGIN
                    SET NOCOUNT ON;
                 
                    DECLARE @EventId uniqueidentifier;
                    DECLARE @IsActive BIT;
                 
                    SELECT @EventId = i.EventId, @IsActive = i.IsActive
                    FROM INSERTED i;
                 
                    IF @IsActive = 0
                    BEGIN
                        UPDATE Pollings
                        SET IsActive = 0
                        WHERE EventId = @EventId;
                    END
                END;
        ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TRIGGER [FinderWebAppDB].[dbo].[trg_UpdatePollingsIsActive]");
        }
    }
}
