
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class CookView : MonoBehaviour
    {
        public UnityAction CallCook = null;
        public UnityAction<CookItem> ServerFood = null;
        private ObjectPool<CookItemView> objectPool = null;
        List<CookItemView> cooks = new List<CookItemView>();
        private Transform parent = null;

        private void Awake()
        {
            parent = this.transform.Find("Content");
            var prefab = Resources.Load<GameObject>("UI/CookItem");
            objectPool = new ObjectPool<CookItemView>(prefab, "CookPool");
        }

        public void UpdataCook(IList<CookItem> cooks)
        {
            for (int i = 0; i < this.cooks.Count; i++)
            {
                objectPool.Push(this.cooks[i]);
            }

            this.cooks.AddRange(objectPool.Pop(cooks.Count));
            ResfrshCook(cooks);
        }

        public void ResfrshCook(IList<CookItem> cooks)
        {
            for (int i = 0; i < this.cooks.Count; i++)
            {
                this.cooks[i].transform.SetParent(parent);
                var item = cooks[i];
                this.cooks[i].transform.Find("Id").GetComponent<Text>().text = item.ToString();
                Color color = this.cooks[i].GetComponent<Image>().color;
                if (item.state.Equals(0))
                {
                    color = Color.green;
                }
                else if (item.state.Equals(1))
                {
                    color = Color.yellow;
                    StartCoroutine(Cooking(cooks[i]));
                }
                else if (item.state.Equals(2))
                {
                    color = Color.red;

                }

                this.cooks[i].GetComponent<Image>().color = color;
            }
        }

        IEnumerator Cooking(CookItem item,float tiem=4)
        {
            item.state = -1;
            yield return new WaitForSeconds(tiem);
            item.state = 0;
            Debug.Log(item.cookOrder);
            ServerFood.Invoke(item);
        }
    }