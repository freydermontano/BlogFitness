using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogFitnessApp.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class updateRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "12345678-1234-1234-1234-123456789012",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f01cc880-c771-4da5-9fe2-0f180184167e", "AQAAAAIAAYagAAAAELtS6wsXtgP6oU8UOZb0jTHtNR1JbWZEjbkchSp2Q6iUxBIcY764OhPNZ43zW5IGCg==", "fb010794-0c5d-4717-89dc-54a2e24c5900" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "c9c7f9f2-3b8a-4d5e-8b1a-6f1e2d3c4b5a", 0, "c2c93f23-0df7-4740-8996-bb1d462d653a", "user1@gmail.com", false, false, null, "user1@gmail.com", "user1@gmail.com", "AQAAAAIAAYagAAAAENPkU3DV9RQPd+1IOCp4yjKycd2TgCDK3SLhQwFgf1V/OBCwxSBdv6UVOSFzZWzocQ==", null, false, "5ee32332-7f0e-458f-af42-a90d681aa823", false, "user1@gmail.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c9c7f9f2-3b8a-4d5e-8b1a-6f1e2d3c4b5a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "12345678-1234-1234-1234-123456789012",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "29a99009-e645-44f3-8232-9cd505eb1241", "AQAAAAIAAYagAAAAEAjCROxnayQBvNK0xJbEXiFUMcjt+sV0//7xfhKYCcKjzUB+RmUJs5ZcYybO/GqG2A==", "da9b34bf-6fbb-437b-a468-bcb9d2a06f14" });
        }
    }
}
