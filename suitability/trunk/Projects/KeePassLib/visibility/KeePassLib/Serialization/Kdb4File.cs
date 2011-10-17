/*
  KeePass Password Safe - The Open-Source Password Manager
  Copyright (C) 2003-2010 Dominik Reichl <dominik.reichl@t-online.de>

  This program is free software; you can redistribute it and/or modify
  it under the terms of the GNU General Public License as published by
  the Free Software Foundation; either version 2 of the License, or
  (at your option) any later version.

  This program is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
  GNU General Public License for more details.

  You should have received a copy of the GNU General Public License
  along with this program; if not, write to the Free Software
  Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
*/

using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.Diagnostics;
using System.Globalization;

#if !KeePassLibSD
using System.IO.Compression;
#endif

using KeePassLib.Cryptography;
using KeePassLib.Interfaces;

namespace KeePassLib.Serialization
{
	/// <summary>
	/// The <c>Kdb4File</c> class supports saving the data to various
	/// formats.
	/// </summary>
	public enum Kdb4Format
	{
		/// <summary>
		/// The default, encrypted file format.
		/// </summary>
		Default = 0,

		/// <summary>
		/// Use this flag when exporting data to a plain-text XML file.
		/// </summary>
		PlainXml
	}

	/// <summary>
	/// Serialization to KeePass KDB files.
	/// </summary>
	public sealed partial class Kdb4File
	{
		/// <summary>
		/// File identifier, first 32-bit value.
		/// </summary>
		internal const uint FileSignature1 = 0x9AA2D903;

		/// <summary>
		/// File identifier, second 32-bit value.
		/// </summary>
		internal const uint FileSignature2 = 0xB54BFB67;

		/// <summary>
		/// File version of files saved by the current <c>Kdb4File</c> class.
		/// KeePass 2.07 has version 1.01, 2.08 has 1.02, 2.09 has 2.00.
		/// The first 2 bytes are critical (i.e. loading will fail, if the
		/// file version is too high), the last 2 bytes are informational.
		/// </summary>
		internal const uint FileVersion32 = 0x00020000;

		internal const uint FileVersionCriticalMask = 0xFFFF0000;

		// KeePass 1.x signature
		internal const uint FileSignatureOld1 = 0x9AA2D903;
		internal const uint FileSignatureOld2 = 0xB54BFB65;
		// KeePass 2.x pre-release (alpha and beta) signature
		internal const uint FileSignaturePreRelease1 = 0x9AA2D903;
		internal const uint FileSignaturePreRelease2 = 0xB54BFB66;

		internal const string ElemDocNode = "KeePassFile";
		internal const string ElemMeta = "Meta";
		internal const string ElemRoot = "Root";
		internal const string ElemGroup = "Group";
		internal const string ElemEntry = "Entry";

		internal const string ElemGenerator = "Generator";
		internal const string ElemDbName = "DatabaseName";
		internal const string ElemDbNameChanged = "DatabaseNameChanged";
		internal const string ElemDbDesc = "DatabaseDescription";
		internal const string ElemDbDescChanged = "DatabaseDescriptionChanged";
		internal const string ElemDbDefaultUser = "DefaultUserName";
		internal const string ElemDbDefaultUserChanged = "DefaultUserNameChanged";
		internal const string ElemDbMntncHistoryDays = "MaintenanceHistoryDays";
		internal const string ElemRecycleBinEnabled = "RecycleBinEnabled";
		internal const string ElemRecycleBinUuid = "RecycleBinUUID";
		internal const string ElemRecycleBinChanged = "RecycleBinChanged";
		internal const string ElemEntryTemplatesGroup = "EntryTemplatesGroup";
		internal const string ElemEntryTemplatesGroupChanged = "EntryTemplatesGroupChanged";
		internal const string ElemLastSelectedGroup = "LastSelectedGroup";
		internal const string ElemLastTopVisibleGroup = "LastTopVisibleGroup";

		internal const string ElemMemoryProt = "MemoryProtection";
		internal const string ElemProtTitle = "ProtectTitle";
		internal const string ElemProtUserName = "ProtectUserName";
		internal const string ElemProtPassword = "ProtectPassword";
		internal const string ElemProtURL = "ProtectURL";
		internal const string ElemProtNotes = "ProtectNotes";
		internal const string ElemProtAutoHide = "AutoEnableVisualHiding";

		internal const string ElemCustomIcons = "CustomIcons";
		internal const string ElemCustomIconItem = "Icon";
		internal const string ElemCustomIconItemID = "UUID";
		internal const string ElemCustomIconItemData = "Data";

		internal const string ElemAutoType = "AutoType";
		internal const string ElemHistory = "History";

		internal const string ElemName = "Name";
		internal const string ElemNotes = "Notes";
		internal const string ElemUuid = "UUID";
		internal const string ElemIcon = "IconID";
		internal const string ElemCustomIconID = "CustomIconUUID";
		internal const string ElemFgColor = "ForegroundColor";
		internal const string ElemBgColor = "BackgroundColor";
		internal const string ElemOverrideUrl = "OverrideURL";
		internal const string ElemTimes = "Times";

		internal const string ElemCreationTime = "CreationTime";
		internal const string ElemLastModTime = "LastModificationTime";
		internal const string ElemLastAccessTime = "LastAccessTime";
		internal const string ElemExpiryTime = "ExpiryTime";
		internal const string ElemExpires = "Expires";
		internal const string ElemUsageCount = "UsageCount";
		internal const string ElemLocationChanged = "LocationChanged";

		internal const string ElemGroupDefaultAutoTypeSeq = "DefaultAutoTypeSequence";
		internal const string ElemEnableAutoType = "EnableAutoType";
		internal const string ElemEnableSearching = "EnableSearching";

		internal const string ElemString = "String";
		internal const string ElemBinary = "Binary";
		internal const string ElemKey = "Key";
		internal const string ElemValue = "Value";

		internal const string ElemAutoTypeEnabled = "Enabled";
		internal const string ElemAutoTypeObfuscation = "DataTransferObfuscation";
		internal const string ElemAutoTypeDefaultSeq = "DefaultSequence";
		internal const string ElemAutoTypeItem = "Association";
		internal const string ElemWindow = "Window";
		internal const string ElemKeystrokeSequence = "KeystrokeSequence";

		internal const string AttrProtected = "Protected";

		internal const string ElemIsExpanded = "IsExpanded";
		internal const string ElemLastTopVisibleEntry = "LastTopVisibleEntry";

		internal const string ElemDeletedObjects = "DeletedObjects";
		internal const string ElemDeletedObject = "DeletedObject";
		internal const string ElemDeletionTime = "DeletionTime";

		internal const string ValFalse = "False";
		internal const string ValTrue = "True";

		internal const string ElemCustomData = "CustomData";
		internal const string ElemStringDictExItem = "Item";

		internal PwDatabase m_pwDatabase = null;

		internal XmlTextWriter m_xmlWriter = null;
		internal CryptoRandomStream m_randomStream = null;
		internal Kdb4Format m_format = Kdb4Format.Default;
		internal IStatusLogger m_slLogger = null;

		internal byte[] m_pbMasterSeed = null;
		internal byte[] m_pbTransformSeed = null;
		internal byte[] m_pbEncryptionIV = null;
		internal byte[] m_pbProtectedStreamKey = null;
		internal byte[] m_pbStreamStartBytes = null;

		// ArcFourVariant only for compatibility; KeePass will default to a
		// different (more secure) algorithm when *writing* databases
		internal CrsAlgorithm m_craInnerRandomStream = CrsAlgorithm.ArcFourVariant;

		internal byte[] m_pbHashOfFileOnDisk = null;

		internal readonly DateTime m_dtNow = DateTime.Now; // Cache current time

		internal const uint NeutralLanguageOffset = 0x100000; // 2^20, see 32-bit Unicode specs
		internal const uint NeutralLanguageIDSec = 0x7DC5C; // See 32-bit Unicode specs
		internal const uint NeutralLanguageID = NeutralLanguageOffset + NeutralLanguageIDSec;
		internal static bool m_bLocalizedNames = false;

		internal enum Kdb4HeaderFieldID : byte
		{
			EndOfHeader = 0,
			Comment = 1,
			CipherID = 2,
			CompressionFlags = 3,
			MasterSeed = 4,
			TransformSeed = 5,
			TransformRounds = 6,
			EncryptionIV = 7,
			ProtectedStreamKey = 8,
			StreamStartBytes = 9,
			InnerRandomStreamID = 10
		}

		public byte[] HashOfFileOnDisk
		{
			get { return m_pbHashOfFileOnDisk; }
		}

		internal bool m_bRepairMode = false;
		public bool RepairMode
		{
			get { return m_bRepairMode; }
			set { m_bRepairMode = value; }
		}

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="pwDataStore">The PwDatabase instance that the class will
		/// load file data into or use to create a KDB file.</param>
		public Kdb4File(PwDatabase pwDataStore)
		{
			Debug.Assert(pwDataStore != null);
			if(pwDataStore == null) throw new ArgumentNullException("pwDataStore");

			m_pwDatabase = pwDataStore;
		}

		/// <summary>
		/// Call this once to determine the current localization settings.
		/// </summary>
		public static void DetermineLanguageId()
		{
			// Test if localized names should be used. If localized names are used,
			// the m_bLocalizedNames value must be set to true. By default, localized
			// names should be used! (Otherwise characters could be corrupted
			// because of different code pages).
			unchecked
			{
				uint uTest = 0;
				foreach(char ch in PwDatabase.LocalizedAppName)
					uTest = uTest * 5 + ch;

				m_bLocalizedNames = (uTest != NeutralLanguageID);
			}
		}
	}
}
