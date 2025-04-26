using UnityEngine;
using Zenject;

namespace RTS.Context
{
    public class MenuContextInstaller : MonoInstaller
    {
        [SerializeField] private HostManager hostManager;
        [SerializeField] private ClientManager clientManager;

        public override void InstallBindings()
        {
            Container.Bind<HostManager>().FromInstance(hostManager);
            Container.Bind<ClientManager>().FromInstance(clientManager);
        }
    }
}
