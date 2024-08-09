using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.GameScene.BottomPanel.TileInfoPanel
{
    public class TileInfoPanelView : MonoBehaviour
    {
        public TMP_Text tileNameText;
        public Image tileImage;

        #region Building Slot Buttons

        public Button smallBuildingTopLeftButton;
        public Button smallBuildingTopRightButton;
        public Button smallBuildingBottomLeftButton;
        public Button smallBuildingBottomRightButton;
        
        public Button mediumBuildingTopButton;
        public Button mediumBuildingBottomButton;
        
        public Button largeBuildingCenterButton;
        public Button extraLargeBuildingCenterButton;
        
        #endregion

        #region Building Slot Sprite Renderers

        public Image smallBuildingTopLeftImage;
        public Image smallBuildingTopRightImage;
        public Image smallBuildingBottomLeftImage;
        public Image smallBuildingBottomRightImage;
        public Image mediumBuildingTopImage;
        public Image mediumBuildingBottomImage;
        public Image largeBuildingCenterImage;
        public Image extraLargeBuildingCenterImage;

        #endregion

        public GameObject resourceGameObject;
        public TMP_Text resourceDetails;
        public Image resourceImage;

        public Transform statsContext;
        public GameObject statsPrefab;
    }
}
