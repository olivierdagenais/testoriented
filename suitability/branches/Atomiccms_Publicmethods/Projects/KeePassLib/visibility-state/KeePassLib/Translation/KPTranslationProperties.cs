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
using System.Text;

namespace KeePassLib.Translation
{
	public sealed class KPTranslationProperties
	{
		internal string m_strApp = string.Empty;
		public string Application
		{
			get { return m_strApp; }
			set { m_strApp = value; }
		}

		internal string m_strForVersion = PwDefs.VersionString;
		public string ApplicationVersion
		{
			get { return m_strForVersion; }
			set { m_strForVersion = value; }
		}

		internal string m_strNameEnglish = string.Empty;
		public string NameEnglish
		{
			get { return m_strNameEnglish; }
			set { m_strNameEnglish = value; }
		}

		internal string m_strNameNative = string.Empty;
		public string NameNative
		{
			get { return m_strNameNative; }
			set { m_strNameNative = value; }
		}

		internal string m_strIso6391Code = string.Empty;
		public string Iso6391Code
		{
			get { return m_strIso6391Code; }
			set { m_strIso6391Code = value; }
		}

		internal bool m_bRtl = false;
		public bool RightToLeft
		{
			get { return m_bRtl; }
			set { m_bRtl = value; }
		}

		internal string m_strAuthorName = string.Empty;
		public string AuthorName
		{
			get { return m_strAuthorName; }
			set { m_strAuthorName = value; }
		}

		internal string m_strAuthorContact = string.Empty;
		public string AuthorContact
		{
			get { return m_strAuthorContact; }
			set { m_strAuthorContact = value; }
		}

		internal string m_strGen = string.Empty;
		public string Generator
		{
			get { return m_strGen; }
			set { m_strGen = value; }
		}

		internal string m_strUuid = string.Empty;
		public string FileUuid
		{
			get { return m_strUuid; }
			set { m_strUuid = value; }
		}

		internal string m_strLastModified = string.Empty;
		public string LastModified
		{
			get { return m_strLastModified; }
			set { m_strLastModified = value; }
		}
	}
}
