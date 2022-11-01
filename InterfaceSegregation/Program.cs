namespace InterfaceSegregation
{
    public interface IMachine
    {
        void Print(Document document);
        void Scan(Document document);
        void Fax(Document document);
    }

    public class Document
    {
    }

    public class MultiFunctionPrinter : IMachine
    {
        public void Print(Document document)
        {
            //
        }

        public void Scan(Document document)
        {
            //
        }

        public void Fax(Document document)
        {
            //
        }
    }

    /*class OldFashionedPrinter : IMachine  => Scanning and Fax not support on old fashioned printers !
    {

    }*/


    //solution
    public interface IPrinter
    {
        void Print(Document document);
    }

    public interface IScanner
    {
        void Scan(Document document);
    }

    public class PhotoCopier:IPrinter,IScanner
    {
        public void Print(Document document)
        {
            // implement
        }

        public void Scan(Document document)
        {
           // implement
        }
    }

    //solution 2
    public interface IMultiFunctionDevice : IScanner, IPrinter
    {

    }

    public class MultiFunctionMachine:IMultiFunctionDevice
    {
        private readonly IPrinter _printer;
        private readonly IScanner _scanner;

        public MultiFunctionMachine(IPrinter printer, IScanner scanner)
        {
            _printer = printer;
            _scanner = scanner;
        }

        public void Print(Document document)
        {
            _printer.Print(document);
        }

        public void Scan(Document document)
        {
            _scanner.Scan(document);
            //decorator pattern example
        }
    }
    internal class Program
    {
        private static void Main()
        {
        }
    }
}