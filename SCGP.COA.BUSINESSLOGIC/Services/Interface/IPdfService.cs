namespace SCGP.COA.BUSINESSLOGIC.Services.Interface
{
    public interface IPdfService
    {
        void MergeMultiplePDFIntoSinglePDF(List<byte[]> pdfFiles, MemoryStream ms, bool pagePerFile = false);
    }
}
