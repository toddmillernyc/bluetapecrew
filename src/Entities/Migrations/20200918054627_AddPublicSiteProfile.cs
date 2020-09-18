using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class AddPublicSiteProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PublicSiteProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SiteTitle = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Keywords = table.Column<string>(nullable: true),
                    Author = table.Column<string>(nullable: true),
                    AboutUs = table.Column<string>(nullable: true),
                    SiteUrl = table.Column<string>(nullable: true),
                    SiteLogoUrl = table.Column<string>(nullable: true),
                    ContactPhoneNumber = table.Column<string>(nullable: true),
                    ContactEmailAddress = table.Column<string>(nullable: true),
                    TwitterUrl = table.Column<string>(nullable: true),
                    FaceBookUrl = table.Column<string>(nullable: true),
                    LinkedInUrl = table.Column<string>(nullable: true),
                    CopyrightText = table.Column<string>(nullable: true),
                    CopyrightUrl = table.Column<string>(nullable: true),
                    CopyrightLinktext = table.Column<string>(nullable: true),
                    FreeShippingThreshold = table.Column<decimal>(nullable: true),
                    FlatShippingRate = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicSiteProfiles", x => x.Id);
                });

            migrationBuilder.Sql($"INSERT INTO [dbo].[PublicSiteProfiles]([SiteTitle] ,[Description] ,[Keywords] ,[Author] ,[AboutUs] ,[SiteUrl] ,[SiteLogoUrl] ,[ContactPhoneNumber] ,[ContactEmailAddress] ,[TwitterUrl] ,[FaceBookUrl] ,[LinkedInUrl] ,[CopyrightText] ,[CopyrightUrl] ,[CopyrightLinktext] ,[FreeShippingThreshold] ,[FlatShippingRate]) VALUES (N'Blue Tape Crew', N'In 2004 Vanessa Bucci was studying at Pratt Institute in Brooklyn. On her time off she was street vending her brand name “NY Bucci” and she had been creating a buzz in the underground art scene of NYC. At the time her focus was selling limited addition Silkscreen Prints and Etchings. She gained notoriety for her artwork that was heavily influenced by industrial landscapes, technology, and electronic music. In 2006 Bucci experimented printing her artwork on t-shirts and apparel. One summer day in Union Square Bucci was selling art and using blue painters tape to secure her display, a fellow street vendor dubbed her “ Blue Tape Crew”. Vanessa liking the sound of the name she then founded “Blue Tape Crew” in New York City in the summer of 2006. “Blue Tape Crew” became a NYC based street wear brand. In 2007 Vanessa Bucci decided to experiment again this time designing, printing and sewing backpacks, purses, and wristlets. She used raw, repurposed or recycled materials to sew one of a kind custom artisanal bags and totes.', N'brooklyn, bushwick, blue tape,bluetapecrew, independent fashion, urban, NYC, Newyorkcity, newyork, streetwear, style, art, bags', N'Todd Miller', N'The Blue Tape Crew was founded in Union Square Park 14th st. in NYC during the summer of 2006. One sunny hot summer day in Union Square while selling prints and other artwork a fellow by the name of T-Bone set up shop next to the artist and nick named the street vendor Blue Tape Crew. After observing the blue painters tape holding down the art. The rest is history.', N'https://dev.bluetapecrew.com', N'https://dev.bluetapecrew.com/content/logo.png', N'718-938-5240', N'bluetapecrew@gmail.com', NULL, N'https://www.facebook.com/Blue-Tape-Crew-135415513184255', NULL, NULL, NULL, NULL, 0.00, 0.00)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PublicSiteProfiles");
        }
    }
}
