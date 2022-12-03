using Controllers;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private UIPanelActivenessController uiPanelController;
        [SerializeField] private GameOverPanelController gameOverPanelController;
        [SerializeField] private LevelPanelController levelPanelController;
        [SerializeField] private HighScorePanelController highScorePanelController;
        //[SerializeField] private StartPanelController startPanelController;

        #endregion

        #endregion

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            UISignals.Instance.onOpenPanel += OnOpenPanel;
            UISignals.Instance.onClosePanel += OnClosePanel;

            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onLevelFailed += OnStageFailed;
            CoreGameSignals.Instance.onLevelFailed += gameOverPanelController.OnStageFailed;
            CoreGameSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
            CoreGameSignals.Instance.onLevelSuccessful += gameOverPanelController.OnStageSuccessFul;
            CoreGameSignals.Instance.onRestartLevel += levelPanelController.OnRestartLevel;
            CoreGameSignals.Instance.onNextLevel += levelPanelController.OnNextLevel;

            ScoreSignals.Instance.onHighScoreChanged += highScorePanelController.OnUpdateText;

            PlayerSignals.Instance.onPlayerInteractedWithCollectable += levelPanelController.OnPlayerInteractedWithCollectable;

        }

        private void UnsubscribeEvents()
        {
            UISignals.Instance.onOpenPanel -= OnOpenPanel;
            UISignals.Instance.onClosePanel -= OnClosePanel;

            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onLevelFailed -= OnStageFailed;
            CoreGameSignals.Instance.onLevelFailed -= gameOverPanelController.OnStageFailed;
            CoreGameSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
            CoreGameSignals.Instance.onLevelSuccessful -= gameOverPanelController.OnStageSuccessFul;
            CoreGameSignals.Instance.onRestartLevel -= levelPanelController.OnRestartLevel;
            CoreGameSignals.Instance.onNextLevel -= levelPanelController.OnNextLevel;

            ScoreSignals.Instance.onHighScoreChanged -= highScorePanelController.OnUpdateText;

            PlayerSignals.Instance.onPlayerInteractedWithCollectable -= levelPanelController.OnPlayerInteractedWithCollectable;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnOpenPanel(UIPanels panelParam)
        {
            uiPanelController.OpenMenu(panelParam);
        }

        private void OnClosePanel(UIPanels panelParam)
        {
            uiPanelController.CloseMenu(panelParam);
        }

        private void OnPlay()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.StartPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.LevelPanel);
        }

        private void OnStageFailed()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.LevelPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.GameOverPanel);
        }

        private void OnLevelSuccessful()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.LevelPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.GameOverPanel);
        }

        public void Play()
        {
            CoreGameSignals.Instance.onPlay?.Invoke();
        }
        public void OptionsButton()
        {
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.OptionsPanel);
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.StartPanel);
        }
    }
}