using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace angularudemydemo.Migrations
{
    public partial class SeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into Makes (Name) values ('Make1')");
            migrationBuilder.Sql("insert into Makes (Name) values ('Make2')");
            migrationBuilder.Sql("insert into Makes (Name) values ('Make3')");

            migrationBuilder.Sql("insert into Model (Name, MakeId) values ('Make1-Model1',(select Id from Makes where Name='Make1'))");
            migrationBuilder.Sql("insert into Model (Name, MakeId) values ('Make1-Model2',(select Id from Makes where Name='Make1'))");
            migrationBuilder.Sql("insert into Model (Name, MakeId) values ('Make1-Model3',(select Id from Makes where Name='Make1'))");
            migrationBuilder.Sql("insert into Model (Name, MakeId) values ('Make2-Model4',(select Id from Makes where Name='Make2'))");
            migrationBuilder.Sql("insert into Model (Name, MakeId) values ('Make2-Model5',(select Id from Makes where Name='Make2'))");
            migrationBuilder.Sql("insert into Model (Name, MakeId) values ('Make2-Model6',(select Id from Makes where Name='Make2'))");
            migrationBuilder.Sql("insert into Model (Name, MakeId) values ('Make3-Model7',(select Id from Makes where Name='Make3'))");
            migrationBuilder.Sql("insert into Model (Name, MakeId) values ('Make3-Model8',(select Id from Makes where Name='Make3'))");
            migrationBuilder.Sql("insert into Model (Name, MakeId) values ('Make3-Model9',(select Id from Makes where Name='Make3'))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.Sql("delete from model where name in ('Make1','Make2','Make3')");
            migrationBuilder.Sql("delete from makes where name in ('Make1','Make2','Make3')");
        }
    }
}
