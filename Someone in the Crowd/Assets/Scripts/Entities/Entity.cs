using SITC.Tools;
using UnityEngine;

namespace SITC
{
    public class Entity : SitcBehaviour
    {
        #region Members
        [SerializeField]
        private SpriteRenderer _face = null;
        [SerializeField]
        private SpriteRenderer _hair = null;
        [SerializeField]
        private SpriteRenderer _head = null;
        [SerializeField]
        private SpriteRenderer _body = null;
        #endregion Members

        #region Private members
        private float _conviction = 0f;
        #endregion Private members

        #region Lifecycle
        protected override void Init()
        {
            base.Init();

            _hair.SetSprite(EntityConfiguration.HairSprites.GetRandom());
            _head.SetSprite(EntityConfiguration.HeadSprites.GetRandom());
            _body.SetSprite(EntityConfiguration.BodySprites.GetRandom());
        }

        protected override void DoUpdate()
        {
            base.DoUpdate();

        }
        #endregion Lifecycle

        #region API
        /// <summary>
        /// Does not normalize vector !
        /// </summary>
        public void Move(Vector3 translation)
        {
            translation *= EntityConfiguration.EntitySpeed;
            translation *= Time.deltaTime;

            transform.position += translation;
            transform.position = transform.position.SetZ(transform.position.y / 100f);

            if (translation.x != 0f)
            {
                bool flipX = translation.x <= 0f;
                _body.FlipX(flipX);
                _hair.FlipX(flipX);
                _face.FlipX(flipX);
                _head.FlipX(flipX);
            }
        }

        public void SetConviction(float conviction)
        {
            _conviction = Mathf.Clamp(conviction, -1f, 1f);
            Sprite sprite;

            if (_conviction < EntityConfiguration.MinimumConvictionForOppressor)
            {
                sprite = EntityConfiguration.OppressorSprite;
            }
            else if (_conviction > EntityConfiguration.MinimumConvictionForResistant)
            {
                sprite = EntityConfiguration.ResistantSprite;
            }
            else
            {
                sprite = EntityConfiguration.NeutralSprite;
            }

            _face.SetSprite(sprite);
        }

        public void AddConviction(float delta)
        {
            SetConviction(_conviction + delta);
        }

        public float GetConviction()
        {
            return _conviction;
        }

        public void Alert()
        {

        }
        #endregion API
    }
}