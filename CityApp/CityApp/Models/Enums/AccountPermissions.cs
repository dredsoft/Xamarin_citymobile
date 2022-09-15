using System;
using System.Diagnostics;

namespace CityApp.Models.Enums
{
	[Flags]
	public enum AccountPermissions
	{
		[DebuggerDisplay("Account Administrator")]
		AccountAdministrator = 1 << 0,

		[DebuggerDisplay("CitationsModel Editor")]
		CitationEdit = 1 << 1,

		/// <summary>
		/// Users who have this permission get their newly created citations auto approved.
		/// This role is for Government employees. 
		/// </summary>
		[DebuggerDisplay("CitationsModel Approver")]
		CitationApprover = 1 << 2,
	}
}
