﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TShockAPI.PermissionSystem
{
	public class PermissionList : IPermissionList
	{
		private List<String> permissions;

		public PermissionList()
		{
			permissions = new List<string>();
		}

		public PermissionList(List<String> permissions)
		{
			this.permissions = permissions;
		}

		public void AddPermission(string permission)
		{
			if (permissions.Contains(permission))
				return;

			permissions.Add(permission);
		}

		public void RemovePermission(string permission)
		{
			if (permissions.Contains(permission))
			{
				permissions.Remove(permission);
			}

			string[] nodes = permission.Split('.');
			for (int i = nodes.Length - 1; i >= 1; i--)
			{
				nodes[i] = "*";
				var testPerm = String.Join(".", nodes, 0, i + 1);
				if (permissions.Contains(testPerm))
				{
					permissions.Remove(testPerm);
				}
			}
		}

		public List<string> GetPermissions()
		{
			return permissions;
		}

		public bool HasPermission(string permission)
		{
			if (String.IsNullOrEmpty(permission) || permissions.Contains(permission))
			{
				return true;
			}

			string[] nodes = permission.Split('.');
			for (int i = nodes.Length - 1; i >= 0; i--)
			{
				nodes[i] = "*";
				if (permissions.Contains(String.Join(".", nodes, 0, i + 1)))
				{
					return true;
				}
			}

			return false;
		}

		public String ToString()
		{
			return String.Join(",", permissions);
		}
	}
}
