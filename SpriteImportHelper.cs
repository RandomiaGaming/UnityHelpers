using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using UnityEditor.Build;

public class SpriteImportHelper : EditorWindow
{
	[MenuItem("Assets/Apply2DPixelArtSettings", false, 1)]
	private static void DoSomething()
	{
		ApplyCustomPixelArtSettings((Texture2D)Selection.activeObject);
	}

	[MenuItem("Assets/Apply2DPixelArtSettings", true, 1)]
	private static bool DoSomethingValidation()
	{
		return Selection.activeObject.GetType() == typeof(Texture2D);
	}

	private static void ApplyCustomPixelArtSettings(Texture2D texture)
	{
		string assetPath = AssetDatabase.GetAssetPath(texture);

		// Get the TextureImporter for the asset path
		TextureImporter textureImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;

		string[] buildTargets = new string[]
{
			NamedBuildTarget.Android.TargetName,
			////NamedBuildTarget.CloudRendering.TargetName,
			NamedBuildTarget.EmbeddedLinux.TargetName,
			NamedBuildTarget.iOS.TargetName,
			NamedBuildTarget.LinuxHeadlessSimulation.TargetName,
			NamedBuildTarget.NintendoSwitch.TargetName,
			NamedBuildTarget.PS4.TargetName,
			NamedBuildTarget.PS5.TargetName,
			NamedBuildTarget.QNX.TargetName,
			NamedBuildTarget.Server.TargetName,
			////NamedBuildTarget.Stadia.TargetName,
			NamedBuildTarget.Standalone.TargetName,
			NamedBuildTarget.tvOS.TargetName,
			NamedBuildTarget.Unknown.TargetName,
			NamedBuildTarget.WebGL.TargetName,
			NamedBuildTarget.WebGL.TargetName,
			NamedBuildTarget.WindowsStoreApps.TargetName,
			NamedBuildTarget.XboxOne.TargetName,
			null,
			"",
			"default",
};

		foreach (string buildTarget in buildTargets)
		{
			textureImporter.ClearPlatformTextureSettings(buildTarget);

			TextureImporterPlatformSettings platformSettings = new TextureImporterPlatformSettings();

			platformSettings.allowsAlphaSplitting = true;
			platformSettings.androidETC2FallbackOverride = AndroidETC2FallbackOverride.UseBuildSettings;
			platformSettings.compressionQuality = 100;
			platformSettings.crunchedCompression = false;
			platformSettings.format = TextureImporterFormat.RGBA32;
			platformSettings.maxTextureSize = 16384;
			platformSettings.name = buildTarget;
			platformSettings.overridden = true;
			platformSettings.resizeAlgorithm = TextureResizeAlgorithm.Mitchell;
			platformSettings.textureCompression = TextureImporterCompression.Uncompressed;

			textureImporter.SetPlatformTextureSettings(platformSettings);
		}

		TextureImporterSettings textureImporterSettings = new TextureImporterSettings();

		textureImporterSettings.alphaIsTransparency = true;
		textureImporterSettings.alphaSource = TextureImporterAlphaSource.FromInput;
		textureImporterSettings.alphaTestReferenceValue = 0.5f;
		textureImporterSettings.aniso = 0;
		textureImporterSettings.borderMipmap = false;
		////textureImporterSettings.compressionQuality = ;
		textureImporterSettings.convertToNormalMap = false;
		textureImporterSettings.cubemapConvolution = TextureImporterCubemapConvolution.None;
		////textureImporterSettings.cubemapConvolutionExponent = ;
		////textureImporterSettings.cubemapConvolutionSteps = ;
		textureImporterSettings.fadeOut = false;
		textureImporterSettings.filterMode = FilterMode.Point;
		textureImporterSettings.flipbookColumns = 0;
		textureImporterSettings.flipbookRows = 0;
		textureImporterSettings.flipGreenChannel = false;
		////textureImporterSettings.generateCubemap = TextureImporterGenerateCubemap.None;
		////textureImporterSettings.generateMipsInLinearSpace = ;
		////textureImporterSettings.grayscaleToAlpha = ;
		textureImporterSettings.heightmapScale = 0.25f;
		textureImporterSettings.ignoreMipmapLimit = false;
		textureImporterSettings.ignorePngGamma = false;
		////textureImporterSettings.lightmap = ;
		////textureImporterSettings.linearTexture = ;
		////textureImporterSettings.maxTextureSize = ;
		textureImporterSettings.mipmapBias = 0;
		textureImporterSettings.mipmapEnabled = false;
		textureImporterSettings.mipmapFadeDistanceEnd = 3;
		textureImporterSettings.mipmapFadeDistanceStart = 1;
		textureImporterSettings.mipmapFilter = TextureImporterMipFilter.BoxFilter;
		textureImporterSettings.mipMapsPreserveCoverage = false;
		////textureImporterSettings.normalMap = ;
		textureImporterSettings.normalMapFilter = TextureImporterNormalFilter.Standard;
		textureImporterSettings.npotScale = TextureImporterNPOTScale.None;
		textureImporterSettings.readable = true;
		////textureImporterSettings.rgbm = ;
		textureImporterSettings.seamlessCubemap = true;
		textureImporterSettings.singleChannelComponent = TextureImporterSingleChannelComponent.Alpha ;
		textureImporterSettings.spriteAlignment = 9;
		textureImporterSettings.spriteBorder = new Vector4(0, 0, 0, 0);
		textureImporterSettings.spriteExtrude = 0;
		textureImporterSettings.spriteGenerateFallbackPhysicsShape = false;
		textureImporterSettings.spriteMeshType = SpriteMeshType.FullRect;
		textureImporterSettings.spriteMode = 1;
		textureImporterSettings.spritePivot = new Vector2(0.5f, 0.5f);
		textureImporterSettings.spritePixelsPerUnit = 16;
		////textureImporterSettings.spritePixelsToUnits = ;
		textureImporterSettings.spriteTessellationDetail = 1;
		textureImporterSettings.sRGBTexture = true;
		textureImporterSettings.streamingMipmaps = false;
		textureImporterSettings.streamingMipmapsPriority = 0;
		textureImporterSettings.swizzleA = TextureImporterSwizzle.A;
		textureImporterSettings.swizzleB = TextureImporterSwizzle.B;
		textureImporterSettings.swizzleG = TextureImporterSwizzle.G;
		textureImporterSettings.swizzleR = TextureImporterSwizzle.R;
		////textureImporterSettings.textureFormat = ;
		textureImporterSettings.textureShape = TextureImporterShape.Texture2D;
		textureImporterSettings.textureType = TextureImporterType.Sprite;
		textureImporterSettings.vtOnly = false;
		textureImporterSettings.wrapMode = TextureWrapMode.Clamp;
		textureImporterSettings.wrapModeU = TextureWrapMode.Clamp;
		textureImporterSettings.wrapModeV = TextureWrapMode.Clamp;
		textureImporterSettings.wrapModeW = TextureWrapMode.Clamp;

		textureImporter.SetTextureSettings(textureImporterSettings);

		textureImporter.allowAlphaSplitting = false;
		textureImporter.alphaIsTransparency = true;
		textureImporter.alphaSource = TextureImporterAlphaSource.FromInput;
		textureImporter.alphaTestReferenceValue = 0.5f;
		textureImporter.androidETC2FallbackOverride = AndroidETC2FallbackOverride.UseBuildSettings;
		textureImporter.anisoLevel = 0;
		////textureImporter.assetBundleName = "";
		////textureImporter.assetBundleVariant = "";
		//textureImporter.assetPath = ;
		//textureImporter.assetTimeStamp = ;
		textureImporter.borderMipmap = false;
		textureImporter.compressionQuality = 100;
		textureImporter.convertToNormalmap = false;
		////textureImporter.correctGamma = false;
		textureImporter.crunchedCompression = false;
		textureImporter.fadeout = false;
		textureImporter.filterMode = FilterMode.Point;
		textureImporter.flipGreenChannel = false;
		////textureImporter.generateCubemap = TextureImporterGenerateCubemap.None;
		////textureImporter.generateMipsInLinearSpace = true;
		////textureImporter.grayscaleToAlpha = false;
		textureImporter.heightmapScale = 0.25f;
		textureImporter.hideFlags = HideFlags.None;
		textureImporter.ignoreMipmapLimit = false;
		textureImporter.ignorePngGamma = false;
		//textureImporter.importSettingsMissing = ;
		textureImporter.isReadable = true;
		////textureImporter.lightmap = false;
		////textureImporter.linearTexture = false;
		textureImporter.maxTextureSize = 16384;
		textureImporter.mipMapBias = 0;
		textureImporter.mipmapEnabled = false;
		textureImporter.mipmapFadeDistanceEnd = 3;
		textureImporter.mipmapFadeDistanceStart = 1;
		textureImporter.mipmapFilter = TextureImporterMipFilter.BoxFilter;
		textureImporter.mipmapLimitGroupName = "";
		textureImporter.mipMapsPreserveCoverage = false;
		textureImporter.name = textureImporter.name;
		////textureImporter.normalmap = false;
		textureImporter.normalmapFilter = TextureImporterNormalFilter.Standard;
		textureImporter.npotScale = TextureImporterNPOTScale.None;
		//textureImporter.qualifiesForSpritePacking = ;
		textureImporter.secondarySpriteTextures = new SecondarySpriteTexture[0];
		textureImporter.spriteBorder = new Vector4(0, 0, 0, 0);
		textureImporter.spriteImportMode = SpriteImportMode.Single;
		////textureImporter.spritePackingTag = "";
		textureImporter.spritePivot = new Vector2(0.5f, 0.5f);
		textureImporter.spritePixelsPerUnit = 16f;
		////textureImporter.spritePixelsToUnits = 16f;
		////textureImporter.spritesheet = new SpriteMetaData[0];
		textureImporter.sRGBTexture = true;
		textureImporter.streamingMipmaps = false;
		textureImporter.streamingMipmapsPriority = 0;
		textureImporter.swizzleA = TextureImporterSwizzle.A;
		textureImporter.swizzleB = TextureImporterSwizzle.B;
		textureImporter.swizzleG = TextureImporterSwizzle.G;
		textureImporter.swizzleR = TextureImporterSwizzle.R;
		textureImporter.textureCompression = TextureImporterCompression.Uncompressed;
		////textureImporter.textureFormat = TextureImporterFormat.RGBA32;
		textureImporter.textureShape = TextureImporterShape.Texture2D;
		textureImporter.textureType = TextureImporterType.Sprite;
		////textureImporter.userData = "";
		textureImporter.vtOnly = false;
		textureImporter.wrapMode = TextureWrapMode.Clamp;
		textureImporter.wrapModeU = TextureWrapMode.Clamp;
		textureImporter.wrapModeV = TextureWrapMode.Clamp;
		textureImporter.wrapModeW = TextureWrapMode.Clamp;

		Debug.Log($"Formatted Asset {assetPath}.");
	}
}
