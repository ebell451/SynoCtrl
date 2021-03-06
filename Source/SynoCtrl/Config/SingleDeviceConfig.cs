﻿using System.Net;
using MSHC.Math.Encryption;
using SynoCtrl.Tasks;
using SynoCtrl.Util;

namespace SynoCtrl.Config
{
	public class SingleDeviceConfig
	{
		private byte[] _macAddress = null;
		private IPAddress _ipAddress = null;
		public readonly string Name;
		public readonly bool AutoGenerated;
		public long? Port = null;
		public bool UseTLS = false;
		public string Username = null;
		public string Password = null;
		
		public string IPAddress
		{
			get => _ipAddress?.ToString();
			set => _ipAddress = ParseIPAddress(value);
		}

		public string MACAddress
		{
			get => EncodingConverter.ByteArrayToHexDump(_macAddress, ":", -1, null, true);
			set => _macAddress = SCUtil.ParseMacAddress(value, true);
		}

		public byte[] MACAddressRaw => _macAddress;

		public IPAddress IPAddressRaw => _ipAddress;

		public SingleDeviceConfig(string n, bool auto)
		{
			Name = n;
			AutoGenerated = auto;
		}

		public void SetIPAddress(IPAddress ip)
		{
			if (_ipAddress != null) throw new TaskException("Internal Error: Cannot override configured IP address");
			_ipAddress = ip;
		}

		public void SetMACAddress(byte[] mac)
		{
			if (_macAddress != null) throw new TaskException("Internal Error: Cannot override configured MAC address");
			_macAddress = mac;
		}

		public void SetPort(int port)
		{
			if (_macAddress != null) throw new TaskException("Internal Error: Cannot override configured MAC address");
			Port = port;
		}

		private static IPAddress ParseIPAddress(string value)
		{
			if (System.Net.IPAddress.TryParse(value, out var result)) return result;
			
			throw new TaskException($"Not a valid IP address: '{value}'");
		}
	}
}
