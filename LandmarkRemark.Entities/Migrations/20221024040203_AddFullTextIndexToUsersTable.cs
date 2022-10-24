using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LandmarkRemark.Entities.Migrations
{
    public partial class AddFullTextIndexToUsersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // EF Core Code First doesn't support creating full text indexes natively.
            migrationBuilder.Sql
            (
                sql: "CREATE FULLTEXT INDEX ON Users(FullName) KEY INDEX PK_Users;",
                suppressTransaction: true
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
