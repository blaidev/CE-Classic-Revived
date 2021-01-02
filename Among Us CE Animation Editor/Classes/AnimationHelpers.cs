﻿using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Numerics;

namespace AmongUsCE_AnimationEditor
{
	public class AnimationHelpers
	{
		public static readonly List<string> DefaultFrameNames = new List<string>()
		{
			"captl_eject",
			"killalien0001",
			"killalien0002",
			"killalien0003",
			"killalien0004",
			"killalien0005",
			"killalien0006",
			"killalien0007",
			"killalien0008",
			"killalien0009",
			"killalien0010",
			"killalien0011",
			"killalien0012",
			"killalien0013",
			"killalien0014",
			"killalien0015",
			"killalien0016",
			"killalien0017",
			"killalien0018",
			"killalien0019",
			"killalien0020",
			"killalien0021",
			"killalien0022",
			"killalien0023",
			"killalien0024",
			"killalien0025",
			"killalien0026",
			"killalien0027",
			"killalien0028",
			"killalien0029",
			"killalien0030",
			"killalien0031",
			"killalien0032",
			"killalien0033",
			"killalien0034",
			"killalien0035",
			"killalien0036",
			"killalien0037",
			"killalien0038",
			"killalien0039",
			"killalien0040",
			"killalien0041",
			"killalien0042",
			"killalien0043",
			"killalien0044",
			"killalien0045",
			"killalien0046",
			"killalien0047",
			"killalien0048",
			"killedalien0001",
			"killedalien0002",
			"killedalien0003",
			"killedalien0004",
			"killedalien0005",
			"killedalien0006",
			"killedalien0007",
			"killedalien0008",
			"killedalien0009",
			"killedalien0010",
			"killedalien0011",
			"killedalien0012",
			"killedalien0013",
			"killedalien0014",
			"killedalien0015",
			"killedalien0016",
			"killedalien0017",
			"killedalien0018",
			"killedalien0019",
			"killedalien0020",
			"killedalien0021",
			"killedalien0022",
			"killedalien0023",
			"killedalien0024",
			"killedalien0025",
			"killedalien0026",
			"killedalien0027",
			"killedalien0028",
			"killedalien0029",
			"killedalien0030",
			"killedalien0031",
			"killedalien0032",
			"killedalien0033",
			"killedalien0034",
			"killedalien0035",
			"killedalien0036",
			"killedalien0037",
			"killedalien0038",
			"killedalien0039",
			"killedalien0040",
			"killedalien0041",
			"killedalien0042",
			"killedalien0043",
			"killedalien0044",
			"killedalien0045",
			"killedalien0046",
			"killedalien0047",
			"killedalien0048",
			"killedgun0001",
			"killedgun0002",
			"killedgun0003",
			"killedgun0004",
			"killedgun0005",
			"killedgun0006",
			"killedgun0007",
			"killedgun0008",
			"killedgun0009",
			"killedgun0010",
			"killedgun0011",
			"killedgun0012",
			"killedgun0013",
			"killedgun0014",
			"killedgun0015",
			"killedgun0016",
			"killedgun0017",
			"killedgun0018",
			"killedgun0019",
			"killedgun0020",
			"killedgun0021",
			"killedgun0022",
			"killedgun0023",
			"killedgun0024",
			"killedgun0025",
			"killedgun0026",
			"killedgun0027",
			"killedgun0028",
			"killedgun0029",
			"killedgun0030",
			"killedgun0031",
			"killedgun0032",
			"killedgun0033",
			"killedgun0034",
			"killedgun0035",
			"killedgun0036",
			"killedgun0037",
			"killedgun0038",
			"killedgun0039",
			"killedgun0040",
			"killedgun0041",
			"killedgun0042",
			"killedgun0043",
			"killedgun0044",
			"killedgun0045",
			"killedgun0046",
			"killedgun0047",
			"killedgun0048",
			"killedneck0001",
			"killedneck0002",
			"killedneck0003",
			"killedneck0004",
			"killedneck0005",
			"killedneck0006",
			"killedneck0007",
			"killedneck0008",
			"killedneck0009",
			"killedneck0010",
			"killedneck0011",
			"killedneck0012",
			"killedneck0013",
			"killedneck0014",
			"killedneck0015",
			"killedneck0016",
			"killedneck0017",
			"killedneck0018",
			"killedneck0019",
			"killedneck0020",
			"killedneck0021",
			"killedneck0022",
			"killedneck0023",
			"killedneck0024",
			"killedneck0025",
			"killedneck0026",
			"killedneck0027",
			"killedneck0028",
			"killedneck0029",
			"killedneck0030",
			"killedneck0031",
			"killedneck0032",
			"killedneck0033",
			"killedneck0034",
			"killedneck0035",
			"killedneck0036",
			"killedneck0037",
			"killedneck0038",
			"killedneck0039",
			"killedneck0040",
			"killedneck0041",
			"killedneck0042",
			"killedneck0043",
			"killedneck0044",
			"killedneck0045",
			"killedneck0046",
			"killedneck0047",
			"killedneck0048",
			"killedstab0001",
			"killedstab0002",
			"killedstab0003",
			"killedstab0004",
			"killedstab0005",
			"killedstab0006",
			"killedstab0007",
			"killedstab0008",
			"killedstab0009",
			"killedstab0010",
			"killedstab0011",
			"killedstab0012",
			"killedstab0013",
			"killedstab0014",
			"killedstab0015",
			"killedstab0016",
			"killedstab0017",
			"killedstab0018",
			"killedstab0019",
			"killedstab0020",
			"killedstab0021",
			"killedstab0022",
			"killedstab0023",
			"killedstab0024",
			"killedstab0025",
			"killedstab0026",
			"killedstab0027",
			"killedstab0028",
			"killedstab0029",
			"killedstab0030",
			"killedstab0031",
			"killedstab0032",
			"killedstab0033",
			"killedstab0034",
			"killedstab0035",
			"killedstab0036",
			"killedstab0037",
			"killedstab0038",
			"killedstab0039",
			"killedstab0040",
			"killedstab0041",
			"killedstab0042",
			"killedstab0043",
			"killedstab0044",
			"killedstab0045",
			"killedstab0046",
			"killedstab0047",
			"killedstab0048",
			"killgun0001",
			"killgun0002",
			"killgun0003",
			"killgun0004",
			"killgun0005",
			"killgun0006",
			"killgun0007",
			"killgun0008",
			"killgun0009",
			"killgun0010",
			"killgun0011",
			"killgun0012",
			"killgun0013",
			"killgun0014",
			"killgun0015",
			"killgun0016",
			"killgun0017",
			"killgun0018",
			"killgun0019",
			"killgun0020",
			"killgun0021",
			"killgun0022",
			"killgun0023",
			"killgun0024",
			"killgun0025",
			"killgun0026",
			"killgun0027",
			"killgun0028",
			"killgun0029",
			"killgun0030",
			"killgun0031",
			"killgun0032",
			"killgun0033",
			"killgun0034",
			"killgun0035",
			"killgun0036",
			"killgun0037",
			"killgun0038",
			"killgun0039",
			"killgun0040",
			"killgun0041",
			"killgun0042",
			"killgun0043",
			"killgun0044",
			"killgun0045",
			"killgun0046",
			"killgun0047",
			"killgun0048",
			"main",
			"spawn0001",
			"spawn0003",
			"spawn0006",
			"spawn0007",
			"spawn0008",
			"spawn0009",
			"spawn0010",
			"spawn0011",
			"spawn0012",
			"spawn0013",
			"spawn0014",
			"spawn0015",
			"spawn0016",
			"spawn0017",
			"spawn0018",
			"spawn0019",
			"spawn0020",
			"spawn0021",
			"spawn0022",
			"spawn0023",
			"spawn0024",
			"spawn0025",
			"spawn0026",
			"spawn0027",
			"spawn0028",
			"spawn0029",
			"spawn0030",
			"spawn0031",
			"spawn0032",
			"spawn0033",
			"spawn0034",
			"spawn0035",
			"spawn0036",
			"spawn0037",
			"spawn0038",
			"spawn0039",
			"spawn0040",
			"spawn0041",
			"spawn0042",
			"spawn0043",
			"spawn0044",
			"spawn0045",
			"spawn0046",
			"spawn0047",
			"spawn0048",
			"spawn0049",
			"spawn0050",
			"spawn0051",
			"spawn0052",
			"vent0001",
			"vent0002",
			"vent0003",
			"vent0004",
			"vent0005",
			"vent0006",
			"vent0007",
			"walk0001",
			"walk0002",
			"walk0003",
			"walk0004",
			"walk0005",
			"walk0006",
			"walk0007",
			"walk0008",
			"walk0009",
			"walk0010",
			"walk0011",
			"walk0012"
		};
		public static string GetFileDirectory(CE_FrameSet File)
        {
			return Path.GetDirectoryName(File.FilePath);
        }
		public static CE_FrameSet Load(FileInfo file)
        {
			StreamReader streamReader =	File.OpenText(file.FullName);
			var result = (CE_FrameSet)new JsonSerializer().Deserialize(streamReader, typeof(CE_FrameSet));
			if (result != null)
			{
				result.FilePath = file.FullName;
			}
			streamReader.Close();
			return result;
		}
		public static void Save(CE_FrameSet save, string filepath = null)
        {
			File.WriteAllText(filepath == null ? save.FilePath : filepath, JsonConvert.SerializeObject(save, Formatting.Indented));
			if (filepath != null)
			{
				save.FilePath = filepath;
			}
		}
		public static Vector2 GetPrecentagePivot(float width, float height, Vector2 pixelCoords)
		{
			float x = pixelCoords.X / width;
			float y = pixelCoords.Y / height;
			Vector2 frame_pivot = new Vector2(x, y);
			return frame_pivot;
		}
		public static Vector2 GetPixelPivot(float width, float height, float x, float y)
		{
			Vector2 pixel_pivot = new Vector2(x * width, y * height);
			return pixel_pivot;
		}
	}

}