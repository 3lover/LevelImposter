using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelImposter.Core;

namespace LevelImposter.Shop
{
    public class MapButton : MonoBehaviour
    {
        private TMPro.TMP_Text titleText;
        private TMPro.TMP_Text descriptionText;
        private TMPro.TMP_Text authorText;
        private TMPro.TMP_Text downloadCountText;
        private PassiveButton button;
        private LIMetadata map;

        public void SetMap(LIMetadata map)
        {
            titleText = transform.Find("Title").GetComponent<TMPro.TMP_Text>();
            descriptionText = transform.Find("Description").GetComponent<TMPro.TMP_Text>();
            authorText = transform.Find("Author").GetComponent<TMPro.TMP_Text>();
            downloadCountText = transform.Find("DownloadCount").GetComponent<TMPro.TMP_Text>();
            button = transform.Find("DeleteButton").GetComponent<PassiveButton>();
            this.map = map;

            titleText.text = map.name;
            descriptionText.text = map.description;
            authorText.text = map.authorID;
            downloadCountText.text = "0"; // TODO

            button.OnClick.RemoveAllListeners();
            button.OnClick.AddListener((Action)DownloadMap);

            gameObject.SetActive(true);
        }

        public void DownloadMap()
        {
            ShopManager.Instance.DownloadMap(map, OnDownloadFinish);
            button.gameObject.SetActive(false);
        }

        public void OnDownloadFinish()
        {
            button.gameObject.SetActive(true);
            button.OnClick.RemoveAllListeners();
            button.OnClick.AddListener((Action)DeleteMap);
        }

        public void DeleteMap()
        {
            ShopManager.Instance.DeleteMap(map.id);
            button.OnClick.RemoveAllListeners();
            button.OnClick.AddListener((Action)DownloadMap);
        }
    }
}