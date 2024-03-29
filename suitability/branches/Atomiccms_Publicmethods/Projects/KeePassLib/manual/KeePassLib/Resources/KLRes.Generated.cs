// This is a generated file!
// Do not edit manually, changes will be overwritten.

using System;
using System.Collections.Generic;

namespace KeePassLib.Resources
{
	/// <summary>
	/// A strongly-typed resource class, for looking up localized strings, etc.
	/// </summary>
	public static class KLRes
	{
		private static string TryGetEx(Dictionary<string, string> dictNew,
			string strName, string strDefault)
		{
			string strTemp;

			if(dictNew.TryGetValue(strName, out strTemp))
				return strTemp;

			return strDefault;
		}

		public static void SetTranslatedStrings(Dictionary<string, string> dictNew)
		{
			if(dictNew == null) throw new ArgumentNullException("dictNew");

			m_strCryptoStreamFailed = TryGetEx(dictNew, "CryptoStreamFailed", m_strCryptoStreamFailed);
			m_strEncAlgorithmAes = TryGetEx(dictNew, "EncAlgorithmAes", m_strEncAlgorithmAes);
			m_strErrorFeedbackRequest = TryGetEx(dictNew, "ErrorFeedbackRequest", m_strErrorFeedbackRequest);
			m_strFatalError = TryGetEx(dictNew, "FatalError", m_strFatalError);
			m_strFatalErrorText = TryGetEx(dictNew, "FatalErrorText", m_strFatalErrorText);
			m_strFileCorrupted = TryGetEx(dictNew, "FileCorrupted", m_strFileCorrupted);
			m_strFileHeaderEndEarly = TryGetEx(dictNew, "FileHeaderEndEarly", m_strFileHeaderEndEarly);
			m_strFileLoadFailed = TryGetEx(dictNew, "FileLoadFailed", m_strFileLoadFailed);
			m_strFileNewVerReq = TryGetEx(dictNew, "FileNewVerReq", m_strFileNewVerReq);
			m_strFileSaveCorruptionWarning = TryGetEx(dictNew, "FileSaveCorruptionWarning", m_strFileSaveCorruptionWarning);
			m_strFileSaveFailed = TryGetEx(dictNew, "FileSaveFailed", m_strFileSaveFailed);
			m_strFileSigInvalid = TryGetEx(dictNew, "FileSigInvalid", m_strFileSigInvalid);
			m_strFileUnknownCipher = TryGetEx(dictNew, "FileUnknownCipher", m_strFileUnknownCipher);
			m_strFileUnknownCompression = TryGetEx(dictNew, "FileUnknownCompression", m_strFileUnknownCompression);
			m_strFileVersionUnsupported = TryGetEx(dictNew, "FileVersionUnsupported", m_strFileVersionUnsupported);
			m_strFinalKeyCreationFailed = TryGetEx(dictNew, "FinalKeyCreationFailed", m_strFinalKeyCreationFailed);
			m_strFrameworkNotImplExcp = TryGetEx(dictNew, "FrameworkNotImplExcp", m_strFrameworkNotImplExcp);
			m_strInvalidCompositeKey = TryGetEx(dictNew, "InvalidCompositeKey", m_strInvalidCompositeKey);
			m_strInvalidCompositeKeyHint = TryGetEx(dictNew, "InvalidCompositeKeyHint", m_strInvalidCompositeKeyHint);
			m_strInvalidDataWhileDecoding = TryGetEx(dictNew, "InvalidDataWhileDecoding", m_strInvalidDataWhileDecoding);
			m_strKeePass1xHint = TryGetEx(dictNew, "KeePass1xHint", m_strKeePass1xHint);
			m_strMasterSeedLengthInvalid = TryGetEx(dictNew, "MasterSeedLengthInvalid", m_strMasterSeedLengthInvalid);
			m_strOldFormat = TryGetEx(dictNew, "OldFormat", m_strOldFormat);
			m_strUnknownHeaderId = TryGetEx(dictNew, "UnknownHeaderId", m_strUnknownHeaderId);
			m_strUserAccountKeyError = TryGetEx(dictNew, "UserAccountKeyError", m_strUserAccountKeyError);
		}

		private static readonly string[] m_vKeyNames = {
			"CryptoStreamFailed",
			"EncAlgorithmAes",
			"ErrorFeedbackRequest",
			"FatalError",
			"FatalErrorText",
			"FileCorrupted",
			"FileHeaderEndEarly",
			"FileLoadFailed",
			"FileNewVerReq",
			"FileSaveCorruptionWarning",
			"FileSaveFailed",
			"FileSigInvalid",
			"FileUnknownCipher",
			"FileUnknownCompression",
			"FileVersionUnsupported",
			"FinalKeyCreationFailed",
			"FrameworkNotImplExcp",
			"InvalidCompositeKey",
			"InvalidCompositeKeyHint",
			"InvalidDataWhileDecoding",
			"KeePass1xHint",
			"MasterSeedLengthInvalid",
			"OldFormat",
			"UnknownHeaderId",
			"UserAccountKeyError"
		};

		public static string[] GetKeyNames()
		{
			return m_vKeyNames;
		}

		private static string m_strCryptoStreamFailed =
			@"Failed to initialize encryption/decryption stream!";
		/// <summary>
		/// Look up a localized string similar to
		/// 'Failed to initialize encryption/decryption stream!'.
		/// </summary>
		public static string CryptoStreamFailed
		{
			get { return m_strCryptoStreamFailed; }
		}

		private static string m_strEncAlgorithmAes =
			@"AES/Rijndael (256-Bit Key)";
		/// <summary>
		/// Look up a localized string similar to
		/// 'AES/Rijndael (256-Bit Key)'.
		/// </summary>
		public static string EncAlgorithmAes
		{
			get { return m_strEncAlgorithmAes; }
		}

		private static string m_strErrorFeedbackRequest =
			@"An extended error report has been copied to the clipboard. Please send it to the KeePass developers.";
		/// <summary>
		/// Look up a localized string similar to
		/// 'An extended error report has been copied to the clipboard. Please send it to the KeePass developers.'.
		/// </summary>
		public static string ErrorFeedbackRequest
		{
			get { return m_strErrorFeedbackRequest; }
		}

		private static string m_strFatalError =
			@"Fatal Error";
		/// <summary>
		/// Look up a localized string similar to
		/// 'Fatal Error'.
		/// </summary>
		public static string FatalError
		{
			get { return m_strFatalError; }
		}

		private static string m_strFatalErrorText =
			@"A fatal error has occurred!";
		/// <summary>
		/// Look up a localized string similar to
		/// 'A fatal error has occurred!'.
		/// </summary>
		public static string FatalErrorText
		{
			get { return m_strFatalErrorText; }
		}

		private static string m_strFileCorrupted =
			@"The file is corrupted.";
		/// <summary>
		/// Look up a localized string similar to
		/// 'The file is corrupted.'.
		/// </summary>
		public static string FileCorrupted
		{
			get { return m_strFileCorrupted; }
		}

		private static string m_strFileHeaderEndEarly =
			@"The file header is corrupted! Some header data was declared but is not present.";
		/// <summary>
		/// Look up a localized string similar to
		/// 'The file header is corrupted! Some header data was declared but is not present.'.
		/// </summary>
		public static string FileHeaderEndEarly
		{
			get { return m_strFileHeaderEndEarly; }
		}

		private static string m_strFileLoadFailed =
			@"Failed to load the specified file!";
		/// <summary>
		/// Look up a localized string similar to
		/// 'Failed to load the specified file!'.
		/// </summary>
		public static string FileLoadFailed
		{
			get { return m_strFileLoadFailed; }
		}

		private static string m_strFileNewVerReq =
			@"A newer KeePass version is required to open this file.";
		/// <summary>
		/// Look up a localized string similar to
		/// 'A newer KeePass version is required to open this file.'.
		/// </summary>
		public static string FileNewVerReq
		{
			get { return m_strFileNewVerReq; }
		}

		private static string m_strFileSaveCorruptionWarning =
			@"The target file might be in a corrupted state. Please try saving again, and if that fails, save the database to a different location.";
		/// <summary>
		/// Look up a localized string similar to
		/// 'The target file might be in a corrupted state. Please try saving again, and if that fails, save the database to a different location.'.
		/// </summary>
		public static string FileSaveCorruptionWarning
		{
			get { return m_strFileSaveCorruptionWarning; }
		}

		private static string m_strFileSaveFailed =
			@"Failed to save the current database to the specified location!";
		/// <summary>
		/// Look up a localized string similar to
		/// 'Failed to save the current database to the specified location!'.
		/// </summary>
		public static string FileSaveFailed
		{
			get { return m_strFileSaveFailed; }
		}

		private static string m_strFileSigInvalid =
			@"The file signature is invalid. Either the file isn't a KeePass database file at all or it is corrupted.";
		/// <summary>
		/// Look up a localized string similar to
		/// 'The file signature is invalid. Either the file isn&#39;t a KeePass database file at all or it is corrupted.'.
		/// </summary>
		public static string FileSigInvalid
		{
			get { return m_strFileSigInvalid; }
		}

		private static string m_strFileUnknownCipher =
			@"The file is encrypted using an unknown encryption algorithm!";
		/// <summary>
		/// Look up a localized string similar to
		/// 'The file is encrypted using an unknown encryption algorithm!'.
		/// </summary>
		public static string FileUnknownCipher
		{
			get { return m_strFileUnknownCipher; }
		}

		private static string m_strFileUnknownCompression =
			@"The file is compressed using an unknown compression algorithm!";
		/// <summary>
		/// Look up a localized string similar to
		/// 'The file is compressed using an unknown compression algorithm!'.
		/// </summary>
		public static string FileUnknownCompression
		{
			get { return m_strFileUnknownCompression; }
		}

		private static string m_strFileVersionUnsupported =
			@"The file version is unsupported.";
		/// <summary>
		/// Look up a localized string similar to
		/// 'The file version is unsupported.'.
		/// </summary>
		public static string FileVersionUnsupported
		{
			get { return m_strFileVersionUnsupported; }
		}

		private static string m_strFinalKeyCreationFailed =
			@"Failed to create the final encryption/decryption key!";
		/// <summary>
		/// Look up a localized string similar to
		/// 'Failed to create the final encryption/decryption key!'.
		/// </summary>
		public static string FinalKeyCreationFailed
		{
			get { return m_strFinalKeyCreationFailed; }
		}

		private static string m_strFrameworkNotImplExcp =
			@"The .NET framework/runtime, under which KeePass is currently running, does not support this operation.";
		/// <summary>
		/// Look up a localized string similar to
		/// 'The .NET framework/runtime, under which KeePass is currently running, does not support this operation.'.
		/// </summary>
		public static string FrameworkNotImplExcp
		{
			get { return m_strFrameworkNotImplExcp; }
		}

		private static string m_strInvalidCompositeKey =
			@"The composite key is invalid!";
		/// <summary>
		/// Look up a localized string similar to
		/// 'The composite key is invalid!'.
		/// </summary>
		public static string InvalidCompositeKey
		{
			get { return m_strInvalidCompositeKey; }
		}

		private static string m_strInvalidCompositeKeyHint =
			@"Make sure the composite key is correct and try again.";
		/// <summary>
		/// Look up a localized string similar to
		/// 'Make sure the composite key is correct and try again.'.
		/// </summary>
		public static string InvalidCompositeKeyHint
		{
			get { return m_strInvalidCompositeKeyHint; }
		}

		private static string m_strInvalidDataWhileDecoding =
			@"Found invalid data while decoding.";
		/// <summary>
		/// Look up a localized string similar to
		/// 'Found invalid data while decoding.'.
		/// </summary>
		public static string InvalidDataWhileDecoding
		{
			get { return m_strInvalidDataWhileDecoding; }
		}

		private static string m_strKeePass1xHint =
			@"In order to import KeePass 1.x KDB files, create a new 2.x database file and click 'File' -> 'Import' in the main menu. In the import dialog, choose 'KeePass KDB (1.x)' as file format.";
		/// <summary>
		/// Look up a localized string similar to
		/// 'In order to import KeePass 1.x KDB files, create a new 2.x database file and click &#39;File&#39; -&gt; &#39;Import&#39; in the main menu. In the import dialog, choose &#39;KeePass KDB (1.x)&#39; as file format.'.
		/// </summary>
		public static string KeePass1xHint
		{
			get { return m_strKeePass1xHint; }
		}

		private static string m_strMasterSeedLengthInvalid =
			@"The length of the master key seed is invalid!";
		/// <summary>
		/// Look up a localized string similar to
		/// 'The length of the master key seed is invalid!'.
		/// </summary>
		public static string MasterSeedLengthInvalid
		{
			get { return m_strMasterSeedLengthInvalid; }
		}

		private static string m_strOldFormat =
			@"The selected file appears to be an old format";
		/// <summary>
		/// Look up a localized string similar to
		/// 'The selected file appears to be an old format'.
		/// </summary>
		public static string OldFormat
		{
			get { return m_strOldFormat; }
		}

		private static string m_strUnknownHeaderId =
			@"Unknown header ID!";
		/// <summary>
		/// Look up a localized string similar to
		/// 'Unknown header ID!'.
		/// </summary>
		public static string UnknownHeaderId
		{
			get { return m_strUnknownHeaderId; }
		}

		private static string m_strUserAccountKeyError =
			@"The operating system did not grant KeePass read/write access to the user profile folder, where the protected user key is stored.";
		/// <summary>
		/// Look up a localized string similar to
		/// 'The operating system did not grant KeePass read/write access to the user profile folder, where the protected user key is stored.'.
		/// </summary>
		public static string UserAccountKeyError
		{
			get { return m_strUserAccountKeyError; }
		}
	}
}
