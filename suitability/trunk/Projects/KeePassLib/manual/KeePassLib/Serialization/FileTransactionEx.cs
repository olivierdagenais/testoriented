﻿/*
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
using System.Text;
using System.IO;
using System.Diagnostics;

#if !KeePassLibSD
using System.Security.AccessControl;
#endif

using KeePassLib.Utility;

namespace KeePassLib.Serialization
{
	public sealed class FileTransactionEx
	{
		private bool m_bTransacted;
		private IOConnectionInfo m_iocBase;
		private IOConnectionInfo m_iocTemp;

		private bool m_bMadeUnhidden = false;

		private const string StrTempSuffix = ".tmp";

		public FileTransactionEx(IOConnectionInfo iocBaseFile)
		{
			Initialize(iocBaseFile, true);
		}

		public FileTransactionEx(IOConnectionInfo iocBaseFile, bool bTransacted)
		{
			Initialize(iocBaseFile, bTransacted);
		}

		private void Initialize(IOConnectionInfo iocBaseFile, bool bTransacted)
		{
			if(iocBaseFile == null) throw new ArgumentNullException("iocBaseFile");

			m_bTransacted = bTransacted;
			m_iocBase = iocBaseFile.CloneDeep();

			if(m_bTransacted)
			{
				m_iocTemp = m_iocBase.CloneDeep();
				m_iocTemp.Path += StrTempSuffix;
			}
			else m_iocTemp = m_iocBase;
		}

		public Stream OpenWrite()
		{
			if(!m_bTransacted) m_bMadeUnhidden = UrlUtil.UnhideFile(m_iocTemp.Path);
			else // m_bTransacted
			{
				try { IOConnection.DeleteFile(m_iocTemp); }
				catch(Exception) { }
			}

			return IOConnection.OpenWrite(m_iocTemp);
		}

		public void CommitWrite()
		{
			if(m_bTransacted) CommitWriteTransaction();
			else // !m_bTransacted
			{
				if(m_bMadeUnhidden) UrlUtil.HideFile(m_iocTemp.Path, true); // Hide again
			}
		}

		private void CommitWriteTransaction()
		{
		    CommitWriteTransaction(m_iocBase, m_iocTemp);
		}

	    internal static void CommitWriteTransaction(IOConnectionInfo iocBase, IOConnectionInfo iocTemp)
	    {
	        bool bMadeUnhidden = UrlUtil.UnhideFile(iocBase.Path);

#if !KeePassLibSD
	        FileSecurity bkSecurity = null;
#endif

	        if(IOConnection.FileExists(iocBase))
	        {
#if !KeePassLibSD
	            if(iocBase.IsLocalFile())
	            {
	                try
	                {
	                    DateTime tCreation = File.GetCreationTime(iocBase.Path);
	                    bkSecurity = File.GetAccessControl(iocBase.Path);

	                    File.SetCreationTime(iocTemp.Path, tCreation);
	                }
	                catch(Exception) { Debug.Assert(false); }
	            }
#endif

	            IOConnection.DeleteFile(iocBase);
	        }

	        IOConnection.RenameFile(iocTemp, iocBase);

#if !KeePassLibSD
	        if(iocBase.IsLocalFile())
	        {
	            try
	            {
	                if(bkSecurity != null)
	                    File.SetAccessControl(iocBase.Path, bkSecurity);
	            }
	            catch(Exception) { Debug.Assert(false); }
            }
#endif

            if (bMadeUnhidden) UrlUtil.HideFile(iocBase.Path, true); // Hide again
	    }
	}
}
