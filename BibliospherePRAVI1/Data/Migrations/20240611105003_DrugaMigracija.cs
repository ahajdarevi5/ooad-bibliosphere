using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibliospherePRAVI1.Data.Migrations
{
    /// <inheritdoc />
    public partial class DrugaMigracija : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Autor",
                columns: table => new
                {
                    autorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    autorIme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    autorPrezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    autorSlika = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    autorDatumRod = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autor", x => x.autorID);
                });

            migrationBuilder.CreateTable(
                name: "Izdavac",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivIzdavaca = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Izdavac", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Korisnik",
                columns: table => new
                {
                    korisnikID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    korisnikIme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    korisnikPrezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    korisnikUsername = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    korisnikEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    korisnikLozinka = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    korisnikDatumRodjenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    brojTelefona = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    citateljDatPrid = table.Column<DateTime>(type: "datetime2", nullable: false),
                    datumZaposlenjaKP = table.Column<DateTime>(type: "datetime2", nullable: false),
                    datumZaposlenjaAD = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnik", x => x.korisnikID);
                });

            migrationBuilder.CreateTable(
                name: "GodisnjiIzvjestaji",
                columns: table => new
                {
                    IzvjestajID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    korisnikID = table.Column<int>(type: "int", nullable: true),
                    Sadrzaj = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    najcitanijiZanr = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GodisnjiIzvjestaji", x => x.IzvjestajID);
                    table.ForeignKey(
                        name: "FK_GodisnjiIzvjestaji_Korisnik_korisnikID",
                        column: x => x.korisnikID,
                        principalTable: "Korisnik",
                        principalColumn: "korisnikID");
                });

            migrationBuilder.CreateTable(
                name: "Knjiga",
                columns: table => new
                {
                    knjigaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ISBN = table.Column<long>(type: "bigint", nullable: false),
                    knjigaNaziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    datumObjavljivanjaKnj = table.Column<DateTime>(type: "datetime2", nullable: false),
                    vrstaKnjige = table.Column<int>(type: "int", nullable: false),
                    knjigaBarKod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    knjigaSlika = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    izdavacID = table.Column<int>(type: "int", nullable: true),
                    korisnikID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Knjiga", x => x.knjigaID);
                    table.ForeignKey(
                        name: "FK_Knjiga_Izdavac_izdavacID",
                        column: x => x.izdavacID,
                        principalTable: "Izdavac",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Knjiga_Korisnik_korisnikID",
                        column: x => x.korisnikID,
                        principalTable: "Korisnik",
                        principalColumn: "korisnikID");
                });

            migrationBuilder.CreateTable(
                name: "MjesecniIzvjestaji",
                columns: table => new
                {
                    IzvjestajID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    korisnikID = table.Column<int>(type: "int", nullable: true),
                    Sadrzaj = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    najcitanijiZanr = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MjesecniIzvjestaji", x => x.IzvjestajID);
                    table.ForeignKey(
                        name: "FK_MjesecniIzvjestaji_Korisnik_korisnikID",
                        column: x => x.korisnikID,
                        principalTable: "Korisnik",
                        principalColumn: "korisnikID");
                });

            migrationBuilder.CreateTable(
                name: "SedmicniIzvjestaji",
                columns: table => new
                {
                    IzvjestajID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    korisnikID = table.Column<int>(type: "int", nullable: true),
                    Sadrzaj = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    najcitanijiZanr = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SedmicniIzvjestaji", x => x.IzvjestajID);
                    table.ForeignKey(
                        name: "FK_SedmicniIzvjestaji_Korisnik_korisnikID",
                        column: x => x.korisnikID,
                        principalTable: "Korisnik",
                        principalColumn: "korisnikID");
                });

            migrationBuilder.CreateTable(
                name: "AutorKnjiga",
                columns: table => new
                {
                    AutorID = table.Column<int>(type: "int", nullable: false),
                    KnjigaID = table.Column<int>(type: "int", nullable: false),
                    AutorKnjigaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutorKnjiga", x => new { x.AutorID, x.KnjigaID });
                    table.ForeignKey(
                        name: "FK_AutorKnjiga_Autor_AutorID",
                        column: x => x.AutorID,
                        principalTable: "Autor",
                        principalColumn: "autorID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AutorKnjiga_Knjiga_KnjigaID",
                        column: x => x.KnjigaID,
                        principalTable: "Knjiga",
                        principalColumn: "knjigaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KnjigaKorisnik",
                columns: table => new
                {
                    knjigaKorisnikID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    korisnikID = table.Column<int>(type: "int", nullable: false),
                    knjigaID = table.Column<int>(type: "int", nullable: true),
                    vrstaListe = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KnjigaKorisnik", x => x.knjigaKorisnikID);
                    table.ForeignKey(
                        name: "FK_KnjigaKorisnik_Knjiga_knjigaID",
                        column: x => x.knjigaID,
                        principalTable: "Knjiga",
                        principalColumn: "knjigaID");
                    table.ForeignKey(
                        name: "FK_KnjigaKorisnik_Korisnik_korisnikID",
                        column: x => x.korisnikID,
                        principalTable: "Korisnik",
                        principalColumn: "korisnikID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recenzija",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ocjena = table.Column<double>(type: "float", nullable: false),
                    Komentar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KnjigaID = table.Column<int>(type: "int", nullable: true),
                    KorisnikID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recenzija", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Recenzija_AspNetUsers_KorisnikID",
                        column: x => x.KorisnikID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Recenzija_Knjiga_KnjigaID",
                        column: x => x.KnjigaID,
                        principalTable: "Knjiga",
                        principalColumn: "knjigaID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AutorKnjiga_KnjigaID",
                table: "AutorKnjiga",
                column: "KnjigaID");

            migrationBuilder.CreateIndex(
                name: "IX_GodisnjiIzvjestaji_korisnikID",
                table: "GodisnjiIzvjestaji",
                column: "korisnikID");

            migrationBuilder.CreateIndex(
                name: "IX_Knjiga_izdavacID",
                table: "Knjiga",
                column: "izdavacID");

            migrationBuilder.CreateIndex(
                name: "IX_Knjiga_korisnikID",
                table: "Knjiga",
                column: "korisnikID");

            migrationBuilder.CreateIndex(
                name: "IX_KnjigaKorisnik_knjigaID",
                table: "KnjigaKorisnik",
                column: "knjigaID");

            migrationBuilder.CreateIndex(
                name: "IX_KnjigaKorisnik_korisnikID",
                table: "KnjigaKorisnik",
                column: "korisnikID");

            migrationBuilder.CreateIndex(
                name: "IX_MjesecniIzvjestaji_korisnikID",
                table: "MjesecniIzvjestaji",
                column: "korisnikID");

            migrationBuilder.CreateIndex(
                name: "IX_Recenzija_KnjigaID",
                table: "Recenzija",
                column: "KnjigaID");

            migrationBuilder.CreateIndex(
                name: "IX_Recenzija_KorisnikID",
                table: "Recenzija",
                column: "KorisnikID");

            migrationBuilder.CreateIndex(
                name: "IX_SedmicniIzvjestaji_korisnikID",
                table: "SedmicniIzvjestaji",
                column: "korisnikID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AutorKnjiga");

            migrationBuilder.DropTable(
                name: "GodisnjiIzvjestaji");

            migrationBuilder.DropTable(
                name: "KnjigaKorisnik");

            migrationBuilder.DropTable(
                name: "MjesecniIzvjestaji");

            migrationBuilder.DropTable(
                name: "Recenzija");

            migrationBuilder.DropTable(
                name: "SedmicniIzvjestaji");

            migrationBuilder.DropTable(
                name: "Autor");

            migrationBuilder.DropTable(
                name: "Knjiga");

            migrationBuilder.DropTable(
                name: "Izdavac");

            migrationBuilder.DropTable(
                name: "Korisnik");
        }
    }
}
