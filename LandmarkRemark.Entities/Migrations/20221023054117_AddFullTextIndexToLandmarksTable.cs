using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LandmarkRemark.Entities.Migrations
{
    public partial class AddFullTextIndexToLandmarksTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // EF Core Code First doesn't support creating full text indexes natively.
            migrationBuilder.Sql
            (
                sql: "CREATE FULLTEXT CATALOG ftCatalog AS DEFAULT;",
                suppressTransaction: true
            );

            migrationBuilder.Sql
            (
                sql: "CREATE FULLTEXT INDEX ON Landmarks(Notes) KEY INDEX PK_Landmarks;",
                suppressTransaction: true
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
