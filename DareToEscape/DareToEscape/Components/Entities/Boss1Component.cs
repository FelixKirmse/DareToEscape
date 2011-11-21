using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlackDragonEngine.Providers;

namespace DareToEscape.Components.Entities
{
    class Boss1Component : BossComponent
    {
        private int _frame;
        private int _frame2;
        private int _frame3;
        private float _angle;

        public override void Update(BlackDragonEngine.Entities.GameObject obj)
        {
            if(Shoot)
            {
                switch (Phase)
                {
                    case 2:
                        if (!VariableProvider.ScriptEngine.IsScriptRunning(Patterns.OngoingBarrage))
                            VariableProvider.ScriptEngine.ExecuteScript(Patterns.OngoingBarrage);
                        break;

                    case 3:
                        if (!VariableProvider.ScriptEngine.IsScriptRunning(Patterns.CurtainBarrage))
                            VariableProvider.ScriptEngine.ExecuteScript(Patterns.CurtainBarrage);
                        break;

                    case 4:
                        if (_frame <= 80)
                            ++_angle;
                        if (_frame >= 80)
                            --_angle;
                        if (_frame == 240)
                            _frame = 0;
                        ++_frame;
                        ++_frame2;
                        ++_frame3;
                        break;
                }
            }
            base.Update(obj);
        }

        protected override IEnumerator<int> ShootBehavior(params float[] parameters)
        {
            switch (Phase)
            {
                case 1:
                    yield return 20;
                    Patterns.ButterflyCircleShot(1);
                    yield return 120;
                    Patterns.Shoot3Circles();
                    yield return 120;
                    yield return 20;
                    Patterns.ButterflyCircleShot(-1);
                    yield return 120;
                    Patterns.Shoot3Circles();
                    yield return 120;
                    break;

                case 2:
                    yield return 120;
                    Patterns.PlayerPrison();
                    yield return 220;
                    StartScript(Patterns.AntiSafeSpotBarrage, 0);
                    StartScript(Patterns.AntiSafeSpotBarrage, 5);
                    StartScript(Patterns.AntiSafeSpotBarrage, -5);
                    StartScript(Patterns.AntiSafeSpotBarrage, -10);
                    StartScript(Patterns.AntiSafeSpotBarrage, 10);
                    StartScript(Patterns.AntiSafeSpotBarrage, -15);
                    StartScript(Patterns.AntiSafeSpotBarrage, 15);
                    StartScript(Patterns.AntiSafeSpotBarrage, -20);
                    StartScript(Patterns.AntiSafeSpotBarrage, 20);
                    yield return 400;
                    break;

                case 3:
                    Patterns.Phase3Shot();
                    yield return 30;
                    break;

                case 4:
                    if (_frame2 == 120)
                    {
                        Patterns.Phase4Frame2120Shot(_angle);
                        _frame2 = 115;
                    }
                    if (_frame3 == 200)
                    {
                        Patterns.Phase4Frame3200Shot();
                        _frame3 = 50;
                    }
                    break;

                case 5:
                    yield return 20;
                    Patterns.ButterflyBarrage2(1);
                    Patterns.ButterflyBarrage2(-1);
                    yield return 120;
                    Patterns.ButterflyBarrage();
                    yield return 120;
                    break;

                case 6:
                    StartScript(Patterns.BulletFlower, 1);
                    yield return 220;
                    StartScript(Patterns.Pentagram, -1);
                    yield return 400;
                    StartScript(Patterns.BulletFlower, -1);
                    yield return 220;
                    StartScript(Patterns.Pentagram, 1);
                    yield return 400;
                    break;

                case 7:
                    yield return 10;
                    StartScript(Patterns.CircleBarrage, 1);
                    StartScript(Patterns.CircleBarrage, 2);
                    yield return 50;
                    StartScript(Patterns.LineBarrage);
                    yield return 70;
                    break;

                case 8:
                    break;
            }
        }

        protected override void SwitchPhase()
        {
            _angle = 0;
            switch (Phase)
            {
                case 1:
                case 3:
                    PhaseTimer = 20 * 60;
                    break;

                case 5:
                case 7:
                    PhaseTimer = 15 * 60;
                    break;
                default:
                    PhaseTimer = 30 * 60;
                    break;
            }
        }
    }
}
