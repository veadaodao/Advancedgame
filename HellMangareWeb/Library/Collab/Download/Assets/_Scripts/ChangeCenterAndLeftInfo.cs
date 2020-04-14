using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCenterAndLeftInfo : MonoBehaviour
{
    public WholeBuildInfoManager wholeBuildInfoManager;
    public int BuildingLevelInfo;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (BuildingLevelInfo)
        {
            case 0:
                wholeBuildInfoManager.LivingBuildingInfoPanel.gameObject.SetActive(false);
                wholeBuildInfoManager.LeftLivingBuildingPanel.gameObject.SetActive(false);

                wholeBuildInfoManager.LivingSleepHouseInfoPanel.gameObject.SetActive(false);
                wholeBuildInfoManager.LeftLiveingCampPanel.gameObject.SetActive(false);


                break;
            case 1:
                wholeBuildInfoManager.LivingBuildingInfoPanel.gameObject.SetActive(true);
                wholeBuildInfoManager.LeftLivingBuildingPanel.gameObject.SetActive(true);

                wholeBuildInfoManager.LivingSleepHouseInfoPanel.gameObject.SetActive(false);
                wholeBuildInfoManager.LeftLiveingCampPanel.gameObject.SetActive(false);

                break;
            case 2:
                wholeBuildInfoManager.LivingSleepHouseInfoPanel.gameObject.SetActive(true);
                wholeBuildInfoManager.LeftLiveingCampPanel.gameObject.SetActive(true);

                wholeBuildInfoManager.LivingBuildingInfoPanel.gameObject.SetActive(false);
                wholeBuildInfoManager.LeftLivingBuildingPanel.gameObject.SetActive(false);
                break;
            case 3:
                break;
            case 4:
                break;
        }

    }
}
