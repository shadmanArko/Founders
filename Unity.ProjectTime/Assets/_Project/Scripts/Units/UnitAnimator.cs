using System;
using UniRx;
using UnityEngine;

namespace _Project.Scripts.Units
{
    public class UnitAnimator : MonoBehaviour
    {
        public Sprite[] disbandSprites;
        public Sprite[] idleSprites;
        public Sprite[] raiseSprites;
        public Sprite[] frontWalkSprites;
        public Sprite[] backWalkSprites;
        public Sprite[] rightWalkSprites;
        public Sprite[] leftWalkSprites;
        public float animationFrameLength  = 0.5f;
        public SpriteRenderer spriteRenderer;
        private float _nextTimeToChangeAnimation = 0f;
        private Sprite[] _selectedSprites;
        private int _cureentSpriteIndex = 0;

        private void Awake()
        {
        }

        private void LoadSprites()
        {
            disbandSprites = Resources.LoadAll<Sprite>($"Map Charecters Animations/{gameObject.name}/disband");
            raiseSprites = Resources.LoadAll<Sprite>($"Map Charecters Animations/{gameObject.name}/raise");
            frontWalkSprites = Resources.LoadAll<Sprite>($"Map Charecters Animations/{gameObject.name}/walk front");
            backWalkSprites = Resources.LoadAll<Sprite>($"Map Charecters Animations/{gameObject.name}/walk back");
            rightWalkSprites =
                Resources.LoadAll<Sprite>($"Map Charecters Animations/{gameObject.name}/walk side right");
            idleSprites = Resources.LoadAll<Sprite>($"Map Charecters Animations/{gameObject.name}/idle");
        }

        private void Start()
        {
            LoadSprites();
            spriteRenderer = GetComponent<UnitView>().spriteRenderer;
            _selectedSprites = idleSprites;
        }

        private void Update()
        {
            if (Time.time > _nextTimeToChangeAnimation)
            {
                ChangeSprite();
                _nextTimeToChangeAnimation = Time.time + animationFrameLength;
            }
            
        }
        private void ChangeSprite()
        {
            spriteRenderer.sprite = _selectedSprites[_cureentSpriteIndex % _selectedSprites.Length];
            _cureentSpriteIndex++;
        }
        public void ShowRaiseAnimation()
        {
            _selectedSprites = raiseSprites;
            _cureentSpriteIndex = 0;
        }
        public void ShowIdleAnimation()
        {
            _selectedSprites = idleSprites;
            _cureentSpriteIndex = 0;
        }
        public void ShowFrontWalkAnimation()
        {
            _selectedSprites = frontWalkSprites;
            _cureentSpriteIndex = 0;
        }
        public void ShowBackWalkAnimation()
        {
            _selectedSprites = backWalkSprites;
            _cureentSpriteIndex = 0;
        }
        public void ShowRightWalkAnimation()
        {
            // transform.localScale = new Vector3(1, 1, 1);
            _selectedSprites = rightWalkSprites;
            _cureentSpriteIndex = 0;
        }
        public void ShowLeftWalkAnimation()
        {
            // transform.localScale = new Vector3(1, 1, 1);
            _selectedSprites = frontWalkSprites; 
            _cureentSpriteIndex = 0;
        }
        public void ShowDisbandAnimation()
        {
            _selectedSprites = disbandSprites;
            _cureentSpriteIndex = 0;
        }

        
    }
}
