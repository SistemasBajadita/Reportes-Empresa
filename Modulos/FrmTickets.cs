﻿using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace Reportes
{
    public partial class FrmTickets : Form
    {
        ClsConnection conn;
        public FrmTickets()
        {
            InitializeComponent();
            conn = new ClsConnection(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            pictureBox1.Image = System.Drawing.Image.FromFile("Imagenes/load.gif");
            pictureBox1.Visible = false;


            FechaA.MaxDate = DateTime.Now;
            FechaB.MaxDate = DateTime.Now;
        }

        private string GetSelectedTextFromCombo()
        {
            if (cmbVendedor.SelectedItem != null)
            {
                DataRowView selectedRow = cmbVendedor.SelectedItem as DataRowView;

                if (selectedRow != null)
                {
                    string selectedText = selectedRow[cmbVendedor.DisplayMember].ToString();
                    return selectedText;
                }
            }
            return null;
        }

        private async void BtnPrintReport_Click(object sender, EventArgs e)
        {
            DataTable tickets = new DataTable();
            string cod = cmbVendedor.SelectedValue.ToString();
            string fechaA = FechaA.Value.ToString("yyyy-MM-dd");
            string fechaB = FechaB.Value.ToString("yyyy-MM-dd");

            await Task.Run(() => tickets = conn.GetQuery($@"select DISTINCT ven.fec_doc, ven.ref_doc, cli.NOM_CLI, hora_reg, concat('$',round(tot_doc,2)), ven.COD_USU, ren.cod_ven
												from tblgralventas ven
												inner join tblcatclientes cli on cli.COD_Cli=ven.COD_CLI
												inner join tblrenventas ren on ren.REF_DOC=ven.REF_DOC
												where CAJA_DOC=9 
												and (ven.FEC_DOC between '{fechaA}' and '{fechaB}')
												and ren.cod_ven='{cod}';"));

            try
            {
                Document doc = new Document();
                string pdfPath = "reporte.pdf";

                using (FileStream fs = new FileStream(pdfPath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    PdfWriter writer = PdfWriter.GetInstance(doc, fs);

                    ClsPageEventHelper pageEventHelper = new ClsPageEventHelper("Imagenes/LOGO_EMPRESA-removebg-preview.png");
                    writer.PageEvent = pageEventHelper;

                    doc.Open();

                    // Título del documento
                    Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);

                    doc.Add(new Paragraph("                         CURLANGO RAMOS CHRISTIAN YARELY \n" +
                        "                         R.F.C CURC890920PW1", titleFont)
                    { Alignment = Element.ALIGN_LEFT });
                    doc.Add(new Paragraph("                         CALLE AVENIDA DE LOS MAESTROS #42 LOCAL 10", titleFont) { Alignment = Element.ALIGN_LEFT });
                    doc.Add(new Paragraph("                         COL. JARDINES DEL BOSQUE", titleFont) { Alignment = Element.ALIGN_LEFT });
                    doc.Add(new Paragraph("                         C.P. 84063 H. NOGALES, SONORA", titleFont) { Alignment = Element.ALIGN_LEFT });

                    doc.Add(new Paragraph("\n"));
                    doc.Add(new Paragraph($"REPORTE DE TICKETS POR CHOFER\nCHOFER: {GetSelectedTextFromCombo()}\nPERIODO: {FechaA.Value.ToString("dd/MM/yyyy")} a {FechaB.Value.ToString("dd/MM/yyyy")}"));
                    doc.Add(new Paragraph("\n"));

                    PdfPTable table = new PdfPTable(6)
                    {
                        WidthPercentage = 100
                    };

                    float[] columnWidths = new float[] { 1f, 2f, 2f, 1f, 1f, 1f };
                    table.SetWidths(columnWidths);

                    Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);
                    PdfPCell headerCell;

                    headerCell = new PdfPCell(new Phrase("FECHA", headerFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 5f };
                    table.AddCell(headerCell);
                    headerCell = new PdfPCell(new Phrase("FOLIO", headerFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 5f };
                    table.AddCell(headerCell);
                    headerCell = new PdfPCell(new Phrase("CLIENTE", headerFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 5f };
                    table.AddCell(headerCell);
                    headerCell = new PdfPCell(new Phrase("HORA", headerFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 5f };
                    table.AddCell(headerCell);
                    headerCell = new PdfPCell(new Phrase("TOTAL", headerFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 5f };
                    table.AddCell(headerCell);
                    headerCell = new PdfPCell(new Phrase("DEV", headerFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 5f };
                    table.AddCell(headerCell);

                    PdfPCell dataCell;
                    Font dataFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);

                    string query = "";
                    double devTotal = 0;

                    foreach (DataRow r in tickets.Rows)
                    {
                        query = $@" select round( SUM(tot_dev),2)
									from tblencdevolucion dev
									inner join tblgralventas ven on dev.REF_DOC=ven.REF_DOC
									where dev.REF_DOC='{r[1]}' and cod_sts=5;";

                        string ro = conn.GetRowQuery(query);

                        if (ro == "")
                        {
                            dataCell = new PdfPCell(new Phrase($"{Convert.ToDateTime(r[0]):dd/MM/yyyy}", dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 5f };
                            table.AddCell(dataCell);
                            dataCell = new PdfPCell(new Phrase($"{r[1]}", dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 2f };
                            table.AddCell(dataCell);
                            dataCell = new PdfPCell(new Phrase($"{r[2]}", dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 2f };
                            table.AddCell(dataCell);
                            dataCell = new PdfPCell(new Phrase($"{r[3]}", dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 2f };
                            table.AddCell(dataCell);
                            dataCell = new PdfPCell(new Phrase($"{r[4]}", dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 2f };
                            table.AddCell(dataCell);
                            dataCell = new PdfPCell(new Phrase($"${0}.00", dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 2f };
                            table.AddCell(dataCell);
                        }
                        else
                        {
                            devTotal += Convert.ToDouble(ro);

                            dataCell = new PdfPCell(new Phrase($"{Convert.ToDateTime(r[0]):dd/MM/yyyy}", dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 5f, BackgroundColor = new BaseColor(230, 133, 138) };
                            table.AddCell(dataCell);
                            dataCell = new PdfPCell(new Phrase($"{r[1]}", dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 2f, BackgroundColor = new BaseColor(230, 133, 138) };
                            table.AddCell(dataCell);
                            dataCell = new PdfPCell(new Phrase($"{r[2]}", dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 2f, BackgroundColor = new BaseColor(230, 133, 138) };
                            table.AddCell(dataCell);
                            dataCell = new PdfPCell(new Phrase($"{r[3]}", dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 2f, BackgroundColor = new BaseColor(230, 133, 138) };
                            table.AddCell(dataCell);
                            dataCell = new PdfPCell(new Phrase($"{r[4]}", dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 2f, BackgroundColor = new BaseColor(230, 133, 138) };
                            table.AddCell(dataCell);
                            dataCell = new PdfPCell(new Phrase($"${Convert.ToDouble(ro):N2}", dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 2f, BackgroundColor = new BaseColor(230, 133, 138) };
                            table.AddCell(dataCell);
                        }
                    }

                    double subtotal = 0;

                    doc.Add(table);

                    foreach (DataRow r in tickets.Rows)
                    {
                        subtotal += double.Parse(r[4].ToString().Substring(1));
                    }


                    doc.Add(new Paragraph($"Subtotal: ${subtotal}", titleFont) { Alignment = Element.ALIGN_RIGHT });
                    doc.Add(new Paragraph($"Devoluciones: ${devTotal}", titleFont) { Alignment = Element.ALIGN_RIGHT });
                    doc.Add(new Paragraph($"Total: ${subtotal - devTotal}", titleFont) { Alignment = Element.ALIGN_RIGHT });

                    doc.Close();
                    writer.Close();
                    Process.Start("reporte.pdf");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "La Bajadita - Venta de Frutas y Verduras");
            }
        }

        private async void FrmTickets_Load(object sender, EventArgs e)
        {
            cmbVendedor.Enabled = false;
            BtnAllTickets.Enabled = false;
            BtnPrintReport.Enabled = false;

            DataTable vendedores = new DataTable();

            await Task.Run(() => vendedores = conn.GetQuery("select cod_ven, nom_ven from tblvendedores where cod_ven!='BORR' " +
                "order by nom_ven asc;"));

            try
            {
                cmbVendedor.DataSource = vendedores;
                cmbVendedor.DisplayMember = "nom_ven";
                cmbVendedor.ValueMember = "cod_ven";
            }
            catch (Exception)
            {
                MessageBox.Show("No se pudieron cargar los vendedores", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            cmbVendedor.Enabled = true;
            BtnAllTickets.Enabled = true;
            BtnPrintReport.Enabled = true;
        }

        private async void BtnAllTickets_Click(object sender, EventArgs e)
        {
            DataTable vendedores = new DataTable();
            string fechaA = FechaA.Value.ToString("yyyy-MM-dd");
            string fechaB = FechaB.Value.ToString("yyyy-MM-dd");
            string query = $@"  select ren.COD_VEN, v.NOM_VEN as Chofer
								from tblrenventas ren
								inner join tblvendedores v on v.COD_VEN=ren.cod_ven
								inner join tblgralventas gral on gral.REF_DOC=ren.REF_DOC
								where (gral.fec_doc between '{fechaA}' and '{fechaB}') and caja_doc=9
								group by ren.cod_ven;";

            BtnAllTickets.Enabled = false;
            BtnPrintReport.Enabled = false;
            cmbVendedor.Enabled = false;
            FechaA.Enabled = false;
            FechaB.Enabled = false;

            pictureBox1.Visible = true;

            await Task.Run(async () =>
            {
                await Task.Run(() => vendedores = conn.GetQuery(query));
                Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
                DataTable tickets = new DataTable();

                double sub = 0;
                double dev = 0;
                double totalDelDia = 0;

                try
                {
                    Document doc = new Document(PageSize.A4, 10, 10, 100, 50);
                    string pdfPath = "reporte de tickets.pdf";

                    using (FileStream fs = new FileStream(pdfPath, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        PdfWriter writer = PdfWriter.GetInstance(doc, fs);

                        ClsHeaderTickets pageEventHelper = new ClsHeaderTickets("Imagenes/LOGO_EMPRESA-removebg-preview.png", "Ventas separadas por vendedor", fechaA, fechaB);
                        writer.PageEvent = pageEventHelper;
                        doc.Open();

                        foreach (DataRow vendedor in vendedores.Rows)
                        {
                            doc.Add(new Paragraph($"Chofer: {vendedor[1]}") { Alignment = Element.ALIGN_CENTER });
                            doc.Add(new Paragraph("\n") { Alignment = Element.ALIGN_CENTER });

                            query = $@"select DISTINCT ven.fec_doc, ven.ref_doc, cli.NOM_CLI, hora_reg, round(tot_doc,2), ven.COD_USU, ren.cod_ven
												from tblgralventas ven
												inner join tblcatclientes cli on cli.COD_Cli=ven.COD_CLI
												inner join tblrenventas ren on ren.REF_DOC=ven.REF_DOC
												where CAJA_DOC=9 
												and (ven.FEC_DOC between '{fechaA}' and '{fechaB}')
												and ren.cod_ven='{vendedor[0]}';";

                            await Task.Run(() => tickets = conn.GetQuery(query));

                            PdfPTable table = new PdfPTable(6);
                            table.WidthPercentage = 100;

                            float[] columnWidths = new float[] { 1f, 2f, 2f, 1f, 1f, 1f };
                            table.SetWidths(columnWidths);

                            Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);
                            PdfPCell headerCell;

                            headerCell = new PdfPCell(new Phrase("FECHA", headerFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 5f };
                            table.AddCell(headerCell);
                            headerCell = new PdfPCell(new Phrase("FOLIO", headerFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 5f };
                            table.AddCell(headerCell);
                            headerCell = new PdfPCell(new Phrase("CLIENTE", headerFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 5f };
                            table.AddCell(headerCell);
                            headerCell = new PdfPCell(new Phrase("HORA", headerFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 5f };
                            table.AddCell(headerCell);
                            headerCell = new PdfPCell(new Phrase("TOTAL", headerFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 5f };
                            table.AddCell(headerCell);
                            headerCell = new PdfPCell(new Phrase("DEV", headerFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 5f };
                            table.AddCell(headerCell);

                            PdfPCell dataCell;
                            Font dataFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);

                            query = "";
                            double devTotal = 0;

                            foreach (DataRow r in tickets.Rows)
                            {
                                query = $@" select round( SUM(tot_dev),2)
									from tblencdevolucion dev
									inner join tblgralventas ven on dev.REF_DOC=ven.REF_DOC
									where dev.REF_DOC='{r[1]}'";

                                string ro = conn.GetRowQuery(query);

                                if (ro == "")
                                {
                                    dataCell = new PdfPCell(new Phrase($"{Convert.ToDateTime(r[0]):dd/MM/yyyy}", dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 5f };
                                    table.AddCell(dataCell);
                                    dataCell = new PdfPCell(new Phrase($"{r[1]}", dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 2f };
                                    table.AddCell(dataCell);
                                    dataCell = new PdfPCell(new Phrase($"{r[2]}", dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 2f };
                                    table.AddCell(dataCell);
                                    dataCell = new PdfPCell(new Phrase($"{r[3]}", dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 2f };
                                    table.AddCell(dataCell);
                                    dataCell = new PdfPCell(new Phrase($"${Convert.ToDouble(r[4]).ToString("N2")}", dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 2f };
                                    table.AddCell(dataCell);
                                    dataCell = new PdfPCell(new Phrase($"${0}.00", dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 2f };
                                    table.AddCell(dataCell);
                                }
                                else
                                {
                                    devTotal += Convert.ToDouble(ro);

                                    dataCell = new PdfPCell(new Phrase($"{Convert.ToDateTime(r[0]):dd/MM/yyyy}", dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 5f, BackgroundColor = new BaseColor(230, 133, 138) };
                                    table.AddCell(dataCell);
                                    dataCell = new PdfPCell(new Phrase($"{r[1]}", dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 2f, BackgroundColor = new BaseColor(230, 133, 138) };
                                    table.AddCell(dataCell);
                                    dataCell = new PdfPCell(new Phrase($"{r[2]}", dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 2f, BackgroundColor = new BaseColor(230, 133, 138) };
                                    table.AddCell(dataCell);
                                    dataCell = new PdfPCell(new Phrase($"{r[3]}", dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 2f, BackgroundColor = new BaseColor(230, 133, 138) };
                                    table.AddCell(dataCell);
                                    dataCell = new PdfPCell(new Phrase($"${Convert.ToDouble(r[4]).ToString("N2")}", dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 2f, BackgroundColor = new BaseColor(230, 133, 138) };
                                    table.AddCell(dataCell);
                                    dataCell = new PdfPCell(new Phrase($"${Convert.ToDouble(ro).ToString("N2")}", dataFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 2f, BackgroundColor = new BaseColor(230, 133, 138) };
                                    table.AddCell(dataCell);
                                }
                            }

                            double subtotal = 0;

                            doc.Add(table);

                            foreach (DataRow r in tickets.Rows)
                            {
                                subtotal += double.Parse(r[4].ToString());
                            }

                            doc.Add(new Paragraph($"Subtotal: ${subtotal:N2}", titleFont) { Alignment = Element.ALIGN_RIGHT });
                            doc.Add(new Paragraph($"Devoluciones: ${devTotal:N2}", titleFont) { Alignment = Element.ALIGN_RIGHT });
                            doc.Add(new Paragraph($"Total: ${(subtotal - devTotal).ToString("N2")}", titleFont) { Alignment = Element.ALIGN_RIGHT });

                            sub += subtotal;
                            dev += devTotal;
                            totalDelDia += subtotal - devTotal;
                        }

                        // Crear una tabla con 2 columnas
                        PdfPTable table1 = new PdfPTable(2)
                        {
                            WidthPercentage = 100 // Ancho de la tabla al 100%
                        };


                        // Agregar las celdas con el texto y las variables
                        table1.AddCell(new PdfPCell(new Phrase("SUBTOTAL DEL DIA:", titleFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.NO_BORDER });
                        table1.AddCell(new PdfPCell(new Phrase($"${sub:N2}", titleFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.NO_BORDER });

                        table1.AddCell(new PdfPCell(new Phrase("DEVOLUCIONES DEL DIA:", titleFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.NO_BORDER });
                        table1.AddCell(new PdfPCell(new Phrase($"${dev:N2}", titleFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.NO_BORDER });

                        table1.AddCell(new PdfPCell(new Phrase("VENTA TOTAL NETA DEL DIA:", titleFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = PdfPCell.NO_BORDER });
                        table1.AddCell(new PdfPCell(new Phrase($"${totalDelDia:N2}", titleFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = PdfPCell.NO_BORDER });

                        doc.Add(table1);

                        doc.Close();
                        writer.Close();
                        Process.Start("reporte de tickets.pdf");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "La Bajadita - Venta de frutas y verduras");
                }
            });

            BtnAllTickets.Enabled = true;
            BtnPrintReport.Enabled = true;
            cmbVendedor.Enabled = true;
            FechaA.Enabled = true;
            FechaB.Enabled = true;
            pictureBox1.Visible = false;
        }
    }
}
