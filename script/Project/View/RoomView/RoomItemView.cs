    
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    
    public class RoomItemView : MonoBehaviour
    {
         private Text text = null;
        private Image image = null;
        public Roomltem roomeitem = null;
        public IList<Action<object>> actionList = new List<Action<object>>();
        private void Awake()
        {
            text = transform.Find("Id").GetComponent<Text>();
            image = transform.GetComponent<Image>();
        }
    
        public void InitClient( Roomltem room )
        {
            this.roomeitem = room;
            UpdateState(); 
        }
    
        private void UpdateState()
        {
            if (roomeitem==null)
            {
                return;
            }
            Color color = Color.green;
    
            switch (this.roomeitem.roomState)
            {
               
               case RoomState.Idle :
                     color = Color.green;
                     break;
                case RoomState.Busy:
                    color = Color.red;
                    StartCoroutine(eatting());
                    break;
               case RoomState.Pay:
                   
                   break;
                
            }
    
            Debug.Log(roomeitem.ToString());
            image.color = color;
            text.text = roomeitem.ToString();
            
        }
    
        
        private IEnumerator eatting( float time = 5 )
        {
            Debug.Log(roomeitem.id + "号桌客人已经入住");
            yield return new WaitForSeconds(time);
            roomeitem.roomState--;
            Debug.Log(roomeitem.id + "客人离开客房");
            actionList[0].Invoke(roomeitem);
        }
    }
