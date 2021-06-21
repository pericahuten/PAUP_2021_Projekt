using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Paup_2021_ServisVozila.Models;
using System.IO;
using System.Web.Hosting;
using Paup_2021_ServisVozila.Misc;

namespace Paup_2021_ServisVozila.Reports
{
    public class RacunListaReport
    {
        public byte[] Podaci { get; private set; }

        private PdfPCell GenerirajCeliju(string sadrzaj, Font font, BaseColor boja, bool wrap)
        {
            PdfPCell c1 = new PdfPCell(new Phrase(sadrzaj, font));
            c1.BackgroundColor = boja;
            c1.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            c1.Padding = 5;
            c1.NoWrap = wrap;
            c1.Border = Rectangle.BOTTOM_BORDER;
            c1.BorderColor = BaseColor.LIGHT_GRAY;
            return c1;
        }

        public void IspisRacunListe(List<Racun> racun, LogiraniKorisnik k)
        {
            BazaDbContext bazapodataka = new BazaDbContext();

            BaseFont bfontZaglavlje = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, false);
            BaseFont bfontTekst = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, false);
            BaseFont bfontPodnozje = BaseFont.CreateFont(BaseFont.TIMES_ITALIC, BaseFont.CP1250, false);

            Font fontZaglavlje = new Font(bfontZaglavlje, 12, Font.NORMAL, BaseColor.DARK_GRAY);
            Font fontZaglavljeBold = new Font(bfontZaglavlje, 12, Font.BOLD, BaseColor.DARK_GRAY);
            Font fontNaslov = new Font(bfontTekst, 14, Font.BOLDITALIC, BaseColor.DARK_GRAY);
            Font fontTablicaZaglavlje = new Font(bfontTekst, 10, Font.BOLD, BaseColor.WHITE);
            Font fontTekst = new Font(bfontTekst, 10, Font.NORMAL, BaseColor.BLACK);
            Font fontTekstBold = new Font(bfontTekst, 10, Font.BOLD, BaseColor.DARK_GRAY);

            BaseColor tPozadinaSadrzaj = BaseColor.WHITE;
            BaseColor tPozadinaZaglavlje = new BaseColor(11, 65, 121);

            using (MemoryStream mstream = new MemoryStream())
            {
                using (Document pdfDokument = new Document(PageSize.A4, 50, 50, 20, 50))
                {
                    PdfWriter.GetInstance(pdfDokument, mstream).CloseStream = false;

                    pdfDokument.Open();

                    PdfPTable tZaglavlje = new PdfPTable(2);
                    tZaglavlje.HorizontalAlignment = Element.ALIGN_LEFT;
                    tZaglavlje.DefaultCell.Border = Rectangle.NO_BORDER;
                    tZaglavlje.WidthPercentage = 100f;
                    float[] sirinaKolonaZag = new float[] { 1f, 3f };
                    tZaglavlje.SetWidths(sirinaKolonaZag);

                    var logo = iTextSharp.text.Image.GetInstance(HostingEnvironment.MapPath("~/Images/gazzz.png"));
                    logo.Alignment = Element.ALIGN_LEFT;
                    logo.ScaleAbsoluteWidth(50);
                    logo.ScaleAbsoluteHeight(50);
                    PdfPCell cLogo = new PdfPCell(logo);
                    cLogo.Border = Rectangle.NO_BORDER;
                    tZaglavlje.AddCell(cLogo);

                    Paragraph info = new Paragraph();
                    info.Alignment = Element.ALIGN_RIGHT;
                    info.SetLeading(0, 1.2f);
                    info.Add(new Chunk("SERVIS VOZILA GAZZZ \n", fontZaglavljeBold));
                    info.Add(new Chunk("Dobriše Cesarića 41b \n Varaždin \n", fontZaglavlje));

                    PdfPCell cInfo = new PdfPCell();
                    cInfo.AddElement(info);
                    cInfo.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cInfo.Border = Rectangle.NO_BORDER;
                    tZaglavlje.AddCell(cInfo);

                    pdfDokument.Add(tZaglavlje);

                    Paragraph pNaslov = new Paragraph("Popis računa", fontNaslov);
                    pNaslov.Alignment = Element.ALIGN_CENTER;
                    pNaslov.SpacingBefore = 20;
                    pNaslov.SpacingAfter = 20;
                    pdfDokument.Add(pNaslov);

                    PdfPTable x = new PdfPTable(5);
                    x.SetWidths(new float[] { 1, 4, 6, 4, 2 });
                    x.WidthPercentage = 100;
                    x.SpacingBefore = 10;

                    x.AddCell(GenerirajCeliju("ID: ", fontTekstBold, tPozadinaSadrzaj, true));
                    x.AddCell(GenerirajCeliju("Vlasnik: ", fontTekstBold, tPozadinaSadrzaj, true));
                    x.AddCell(GenerirajCeliju("Datum: ", fontTekstBold, tPozadinaSadrzaj, true));
                    x.AddCell(GenerirajCeliju("Izdao: ", fontTekstBold, tPozadinaSadrzaj, true));
                    x.AddCell(GenerirajCeliju("Cijena: ", fontTekstBold, tPozadinaSadrzaj, true));

                    foreach (var r in racun)
                    {
                        x.AddCell(GenerirajCeliju(r.id.ToString(), fontTekst, tPozadinaSadrzaj, false));
                        x.AddCell(GenerirajCeliju(r.servisiFK.idAuto.Vlasnik.PrezimeIme, fontTekst, tPozadinaSadrzaj, false));
                        x.AddCell(GenerirajCeliju(r.Datum.ToString("dd.MM.yyyy"), fontTekst, tPozadinaSadrzaj, false));
                        x.AddCell(GenerirajCeliju(r.osoba, fontTekst, tPozadinaSadrzaj, false));
                        x.AddCell(GenerirajCeliju(r.cijena.ToString() + " kn", fontTekst, tPozadinaSadrzaj, false));
                    }

                    pdfDokument.Add(x);
                    Paragraph pMjesto = new Paragraph(DateTime.Now.ToString("dd.MM.yyyy"), fontTekst);
                    pMjesto.Alignment = Element.ALIGN_RIGHT;
                    pdfDokument.Add(pMjesto);
                    Paragraph pdfKreirao = new Paragraph("Kreirao: " + k.PrezimeIme, fontTekst);
                    pdfKreirao.Alignment = Element.ALIGN_RIGHT;
                    pdfDokument.Add(pdfKreirao);
                }
                Podaci = mstream.ToArray();
            }

        }
    }
}