using ESCPOS_NET;

namespace Resto.Infrastructure.Integrations.TicketPrinting;

/// <summary>
/// This implementation of BasePrinter copies over the implementation of FilePrinter
/// in the ESCPOS_NET library, but changes the creation of the FileStream to specify
/// write-only and WriteThrough since it resulted in faulty prints.
/// Once this issue is fixed upstream, this implementation should be made obsolete.
/// </summary>
// internal sealed class FileStreamPrinter : BasePrinter
// {
//     private readonly FileStream _fileStream;
//
//     public FileStreamPrinter(string filePath, bool createIfNotExists = false) : base()
//     {
//         _fileStream = new FileStream(
//             filePath,
//             createIfNotExists ? FileMode.OpenOrCreate : FileMode.Open,
//             FileAccess.Write,
//             FileShare.None,
//             4096,
//             FileOptions.WriteThrough);
//         Writer = new BinaryWriter(_fileStream);
//         // Reader = new BinaryReader(_fileStream);
//     }
//
//     ~FileStreamPrinter() => Dispose(false);
//
//     protected override void OverridableDispose()
//     {
//         _fileStream.Close();
//         _fileStream?.Dispose();
//     }
// }