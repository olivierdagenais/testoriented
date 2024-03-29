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
using System.Diagnostics;

using KeePassLib.Security;

namespace KeePassLib.Cryptography.PasswordGenerator
{
	internal static class CharSetBasedGenerator
	{
		public static PwgError Generate(ProtectedString psOutBuffer,
			PwProfile pwProfile, CryptoRandomStream crsRandomSource)
		{
			if(pwProfile.Length == 0) return PwgError.Success;

			PwCharSet pcs = new PwCharSet(pwProfile.CharSet.ToString());
			char[] vGenerated = new char[pwProfile.Length];

			PwGenerator.PrepareCharSet(pcs, pwProfile);

			for(int nIndex = 0; nIndex < (int)pwProfile.Length; ++nIndex)
			{
				char ch = PwGenerator.GenerateCharacter(pwProfile, pcs,
					crsRandomSource);

				if(ch == char.MinValue)
				{
					Array.Clear(vGenerated, 0, vGenerated.Length);
					return PwgError.TooFewCharacters;
				}

				vGenerated[nIndex] = ch;
			}

			byte[] pbUTF8 = Encoding.UTF8.GetBytes(vGenerated);
			psOutBuffer.SetString(Encoding.UTF8.GetString(pbUTF8, 0, pbUTF8.Length));
			Array.Clear(pbUTF8, 0, pbUTF8.Length);
			Array.Clear(vGenerated, 0, vGenerated.Length);

			return PwgError.Success;
		}
	}
}
