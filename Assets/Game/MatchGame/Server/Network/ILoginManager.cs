 // This asset was uploaded by https://unityassetcollection.com


namespace POPBlocks.Server.Network
{
	public interface ILoginManager {
		void LoginWithFB (string accessToken, string titleId);

		void UpdateName (string userName);

		bool IsYou (string playFabId);
	}
}


