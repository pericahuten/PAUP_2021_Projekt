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
    public class RacunReport
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


        public void IspisRacuna(Racun r, List<RacunStavke> rs, LogiraniKorisnik k)
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

                    Paragraph info = new Paragraph();
                    info.Alignment = Element.ALIGN_RIGHT;
                    info.SetLeading(0, 1.2f);
                    info.Add(new Chunk("PAUP 2021 Projekt \n", fontZaglavljeBold));
                    info.Add(new Chunk("Servis vozila \n", fontZaglavlje));

                    PdfPCell cInfo = new PdfPCell();
                    cInfo.AddElement(info);
                    cInfo.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cInfo.Border = Rectangle.NO_BORDER;
                    tZaglavlje.AddCell(cInfo);

                    pdfDokument.Add(tZaglavlje);

                    Paragraph pNaslov = new Paragraph("Podaci o računu", fontNaslov);
                    pNaslov.Alignment = Element.ALIGN_CENTER;
                    pNaslov.SpacingBefore = 20;
                    pNaslov.SpacingAfter = 20;
                    pdfDokument.Add(pNaslov);

                    PdfPTable x = new PdfPTable(2);
                    x.SetWidths(new float[] { 2, 6 });
                    x.WidthPercentage = 100;
                    x.SpacingBefore = 10;


                    x.AddCell(GenerirajCeliju("Vozilo: ", fontTekstBold, tPozadinaSadrzaj, true));
                    x.AddCell(GenerirajCeliju(r.servisiFK.idAuto.Registracija, fontTekst, tPozadinaSadrzaj, false));
                    x.AddCell(GenerirajCeliju("Datum: ", fontTekstBold, tPozadinaSadrzaj, true));
                    x.AddCell(GenerirajCeliju(r.Datum.ToString("dd.MM.yyyy"), fontTekst, tPozadinaSadrzaj, false));
                    x.AddCell(GenerirajCeliju("Ovlasteni serviser: ", fontTekstBold, tPozadinaSadrzaj, true));
                    x.AddCell(GenerirajCeliju(r.osoba, fontTekst, tPozadinaSadrzaj, false));
                    pdfDokument.Add(x);

                    PdfPTable t = new PdfPTable(4);
                    t.WidthPercentage = 100;
                    t.SetWidths(new float[] { 2, 3, 1, 1 });

                    t.AddCell(GenerirajCeliju("Rbr stavke", fontTablicaZaglavlje, tPozadinaZaglavlje, true));
                    t.AddCell(GenerirajCeliju("Artikl", fontTablicaZaglavlje, tPozadinaZaglavlje, true));
                    t.AddCell(GenerirajCeliju("Količina", fontTablicaZaglavlje, tPozadinaZaglavlje, true));
                    t.AddCell(GenerirajCeliju("Iznos", fontTablicaZaglavlje, tPozadinaZaglavlje, true));
                    foreach (var stavka in rs)
                    {
                        t.AddCell(GenerirajCeliju(stavka.redniBroj.ToString(), fontTekst, tPozadinaSadrzaj, false));
                        t.AddCell(GenerirajCeliju(stavka.ArtiklFK.Naziv.ToString(), fontTekst, tPozadinaSadrzaj, false));
                        t.AddCell(GenerirajCeliju(stavka.kolicina.ToString(), fontTekst, tPozadinaSadrzaj, false));
                        t.AddCell(GenerirajCeliju(stavka.iznos.ToString()+" kn", fontTekst, tPozadinaSadrzaj, false));
                    }

                    pdfDokument.Add(t);

                    Paragraph pCijena = new Paragraph("Ukupna cijena: " + r.cijena + " kn", fontTekstBold);
                    pCijena.Alignment = Element.ALIGN_RIGHT;
                    pCijena.SpacingAfter = 30;
                    pdfDokument.Add(pCijena);
                    Paragraph pMjesto = new Paragraph(DateTime.Now.ToString("dd.MM.yyyy"), fontTekst);
                    pMjesto.Alignment = Element.ALIGN_RIGHT;
                    pCijena.SpacingBefore = 30;
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