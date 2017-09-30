using SITC.Tools;
using UnityEngine;

namespace SITC
{
    public class Entity : SitcBehaviour
    {
        #region Members
        [Header("Visual"), SerializeField]
        private SpriteRenderer _face = null;
        [SerializeField]
        private SpriteRenderer _hair = null;
        [SerializeField]
        private SpriteRenderer _head = null;
        [SerializeField]
        private SpriteRenderer _body = null;

        [Header("Visual copy"), SerializeField]
        private SpriteRenderer _faceC = null;
        [SerializeField]
        private SpriteRenderer _hairC = null;
        [SerializeField]
        private SpriteRenderer _headC = null;
        [SerializeField]
        private SpriteRenderer _bodyC = null;
        #endregion Members

        #region Private members
        private float _conviction = 0f;
        #endregion Private members

        #region Lifecycle
        protected override void Init()
        {
            base.Init();

            var s = EntityConfiguration.HairSprites.GetRandom();
            _hair.SetSprite(s);
            _hairC.SetSprite(s);

            s = EntityConfiguration.HeadSprites.GetRandom();
            _head.SetSprite(s);
            _headC.SetSprite(s);

            s = EntityConfiguration.BodySprites.GetRandom();
            _body.SetSprite(s);
            _bodyC.SetSprite(s);
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

                _bodyC.FlipX(flipX);
                _hairC.FlipX(flipX);
                _faceC.FlipX(flipX);
                _headC.FlipX(flipX);
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
            _faceC.SetSprite(sprite);

            Color c;

            if (_conviction == 0f)
            {
                c = EntityConfiguration.NeutralColor;
            }
            else if (_conviction > 0f)
            {
                c = Color.Lerp(EntityConfiguration.NeutralColor, EntityConfiguration.ResistantColor, _conviction);
            }
            else
            {
                c = Color.Lerp(EntityConfiguration.NeutralColor, EntityConfiguration.ConvictionColor, -_conviction);
            }

            _bodyC.SetColor(c);
            _hairC.SetColor(c);
            _faceC.SetColor(c);
            _headC.SetColor(c);
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