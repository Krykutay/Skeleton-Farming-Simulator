using UnityEngine;
using UnityEngine.UI;

namespace AllIn1SpriteShader
{
    public class All1ShaderDemoController : MonoBehaviour
    {
        [SerializeField] private DemoCircleExpositor[] expositors = null;
        [SerializeField] private Text expositorsTitle = null, expositorsTitleOutline = null;
        public float expositorDistance;

        private int currExpositor;

        [SerializeField] private GameObject background = null;
        private Material backgroundMat;
        [SerializeField] private float colorLerpSpeed = 0;
        private Color[] targetColors;
        private Color[] currentColors;

        void Start()
        {
            currExpositor = 0;
            SetExpositorText();

            for (int i = 0; i < expositors.Length; i++) expositors[i].transform.position = new Vector3(0, expositorDistance * i, 0);

            backgroundMat = background.GetComponent<Image>().material;
            targetColors = new Color[4];
            targetColors[0] = backgroundMat.GetColor("_GradTopLeftCol");
            targetColors[1] = backgroundMat.GetColor("_GradTopRightCol");
            targetColors[2] = backgroundMat.GetColor("_GradBotLeftCol");
            targetColors[3] = backgroundMat.GetColor("_GradBotRightCol");
            currentColors = targetColors.Clone() as Color[];
        }

        void Update()
        {
            GetInput();

            currentColors[0] = Color.Lerp(currentColors[0], targetColors[(0 + currExpositor) % targetColors.Length], colorLerpSpeed * Time.deltaTime);
            currentColors[1] = Color.Lerp(currentColors[1], targetColors[(1 + currExpositor) % targetColors.Length], colorLerpSpeed * Time.deltaTime);
            currentColors[2] = Color.Lerp(currentColors[2], targetColors[(2 + currExpositor) % targetColors.Length], colorLerpSpeed * Time.deltaTime);
            currentColors[3] = Color.Lerp(currentColors[3], targetColors[(3 + currExpositor) % targetColors.Length], colorLerpSpeed * Time.deltaTime);
            backgroundMat.SetColor("_GradTopLeftCol", currentColors[0]);
            backgroundMat.SetColor("_GradTopRightCol", currentColors[1]);
            backgroundMat.SetColor("_GradBotLeftCol", currentColors[2]);
            backgroundMat.SetColor("_GradBotRightCol", currentColors[3]);
        }

        private void GetInput()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                expositors[currExpositor].ChangeTarget(-1);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                expositors[currExpositor].ChangeTarget(1);
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                ChangeExpositor(-1);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                ChangeExpositor(1);
            }
        }

        private void ChangeExpositor(int offset)
        {
            currExpositor += offset;
            if (currExpositor > expositors.Length - 1) currExpositor = 0;
            else if (currExpositor < 0) currExpositor = expositors.Length - 1;
            SetExpositorText();
        }

        private void SetExpositorText()
        {
            expositorsTitle.text = expositors[currExpositor].name;
            expositorsTitleOutline.text = expositors[currExpositor].name;
        }

        public int GetCurrExpositor() { return currExpositor; }
    }
}