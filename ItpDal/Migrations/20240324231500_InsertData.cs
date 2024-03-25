using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItpDal.Migrations
{
    public partial class InsertData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "CreateDate", "ProjectName", "UpdateDate" },
                values: new object[] { new Guid("287b58cb-ba89-4ec9-92e8-7507cade9a7e"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ProjectName3", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "CreateDate", "ProjectName", "UpdateDate" },
                values: new object[] { new Guid("397177b8-c22c-4c29-96be-4a4723137b9c"), new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ProjectName1", new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "CreateDate", "ProjectName", "UpdateDate" },
                values: new object[] { new Guid("ade417eb-8e9d-498d-83a9-52bb67af2832"), new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "ProjectName2", new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "CancelDate", "CreateDate", "EndDate", "ProjectId", "StartDate", "TaskDescription", "TaskName", "UpdateDate" },
                values: new object[] { new Guid("22069035-1318-4688-bbbc-71369de71aa5"), null, new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 2, 0, 1, 1, 1, DateTimeKind.Unspecified), new Guid("287b58cb-ba89-4ec9-92e8-7507cade9a7e"), new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "TaskName3", new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "CancelDate", "CreateDate", "EndDate", "ProjectId", "StartDate", "TaskDescription", "TaskName", "UpdateDate" },
                values: new object[] { new Guid("2f33204d-8af2-4515-8e89-c8c040faf0d5"), null, new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 2, 0, 1, 1, 1, DateTimeKind.Unspecified), new Guid("287b58cb-ba89-4ec9-92e8-7507cade9a7e"), new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "TaskName2", new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "CancelDate", "CreateDate", "EndDate", "ProjectId", "StartDate", "TaskDescription", "TaskName", "UpdateDate" },
                values: new object[] { new Guid("36758734-e101-48ed-ad26-d83e6b20ecf8"), null, new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 2, 0, 1, 1, 1, DateTimeKind.Unspecified), new Guid("ade417eb-8e9d-498d-83a9-52bb67af2832"), new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "TaskName2", new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "CancelDate", "CreateDate", "EndDate", "ProjectId", "StartDate", "TaskDescription", "TaskName", "UpdateDate" },
                values: new object[] { new Guid("4987229d-40c6-48df-9ee6-15f204995d01"), null, new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 2, 0, 1, 1, 1, DateTimeKind.Unspecified), new Guid("397177b8-c22c-4c29-96be-4a4723137b9c"), new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "TaskName3", new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "CancelDate", "CreateDate", "EndDate", "ProjectId", "StartDate", "TaskDescription", "TaskName", "UpdateDate" },
                values: new object[] { new Guid("5b5f1295-e233-4b9a-a220-68ad93d06918"), null, new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 2, 0, 1, 1, 1, DateTimeKind.Unspecified), new Guid("397177b8-c22c-4c29-96be-4a4723137b9c"), new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "TaskName2", new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "CancelDate", "CreateDate", "EndDate", "ProjectId", "StartDate", "TaskDescription", "TaskName", "UpdateDate" },
                values: new object[] { new Guid("8db1a244-4382-4a21-a050-afa89bde26c8"), null, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 1, 0, 1, 1, 1, DateTimeKind.Unspecified), new Guid("287b58cb-ba89-4ec9-92e8-7507cade9a7e"), new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "TaskName1", new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "CancelDate", "CreateDate", "EndDate", "ProjectId", "StartDate", "TaskDescription", "TaskName", "UpdateDate" },
                values: new object[] { new Guid("ae469f73-d324-4447-beab-718acaa85b36"), null, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 1, 0, 1, 1, 1, DateTimeKind.Unspecified), new Guid("397177b8-c22c-4c29-96be-4a4723137b9c"), new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "TaskName1", new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "CancelDate", "CreateDate", "EndDate", "ProjectId", "StartDate", "TaskDescription", "TaskName", "UpdateDate" },
                values: new object[] { new Guid("affa7ae8-d32d-4c67-98b7-33b276363796"), null, new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 2, 0, 1, 1, 1, DateTimeKind.Unspecified), new Guid("ade417eb-8e9d-498d-83a9-52bb67af2832"), new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "TaskName3", new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "CancelDate", "CreateDate", "EndDate", "ProjectId", "StartDate", "TaskDescription", "TaskName", "UpdateDate" },
                values: new object[] { new Guid("f6dd65d7-b740-4b1f-9c76-fc0decae3602"), null, new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 3, 0, 1, 1, 1, DateTimeKind.Unspecified), new Guid("ade417eb-8e9d-498d-83a9-52bb67af2832"), new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "TaskName1", new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "TaskComments",
                columns: new[] { "Id", "CommentType", "Content", "TaskId" },
                values: new object[] { new Guid("b9aef972-5869-4939-b92d-eef6426bcc1f"), (byte)1, new byte[] { 84, 104, 105, 115, 32, 105, 115, 32, 97, 32, 116, 101, 115, 116, 32, 50 }, new Guid("5b5f1295-e233-4b9a-a220-68ad93d06918") });

            migrationBuilder.InsertData(
                table: "TaskComments",
                columns: new[] { "Id", "CommentType", "Content", "TaskId" },
                values: new object[] { new Guid("e1834198-2a04-475d-9201-56250f781dfb"), (byte)1, new byte[] { 84, 104, 105, 115, 32, 105, 115, 32, 97, 32, 116, 101, 115, 116, 32, 49 }, new Guid("ae469f73-d324-4447-beab-718acaa85b36") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TaskComments",
                keyColumn: "Id",
                keyValue: new Guid("b9aef972-5869-4939-b92d-eef6426bcc1f"));

            migrationBuilder.DeleteData(
                table: "TaskComments",
                keyColumn: "Id",
                keyValue: new Guid("e1834198-2a04-475d-9201-56250f781dfb"));

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("22069035-1318-4688-bbbc-71369de71aa5"));

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("2f33204d-8af2-4515-8e89-c8c040faf0d5"));

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("36758734-e101-48ed-ad26-d83e6b20ecf8"));

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("4987229d-40c6-48df-9ee6-15f204995d01"));

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("8db1a244-4382-4a21-a050-afa89bde26c8"));

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("affa7ae8-d32d-4c67-98b7-33b276363796"));

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("f6dd65d7-b740-4b1f-9c76-fc0decae3602"));

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: new Guid("287b58cb-ba89-4ec9-92e8-7507cade9a7e"));

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: new Guid("ade417eb-8e9d-498d-83a9-52bb67af2832"));

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("5b5f1295-e233-4b9a-a220-68ad93d06918"));

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("ae469f73-d324-4447-beab-718acaa85b36"));

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: new Guid("397177b8-c22c-4c29-96be-4a4723137b9c"));
        }
    }
}
