import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultsService{

    async getById(id) {
        logger.log(id)
        const res = await api.get(`api/vaults/${id}`)
        logger.log(res.data)
        AppState.activeVault = res.data
    }

    async createVault(body) {
        const res = await api.post('api/vaults', body)
        logger.log(res.data)
        AppState.profileVaults.unshift(res.data)
        
    }
    async getKeepsByVaultId(id) {
        const res = await api.get(`api/vaults/${id}/keeps`)
        logger.log(res.data)
        AppState.vaultKeeps = res.data
        
    }
    async getMyVaults() {
        const res = await api.get(`account/vaults`)
        logger.log(res.data)
        AppState.vaults = res.data
    }

    async createVaultKeep(data) {
        const res = await api.post('api/vaultkeeps', data)
        logger.log(res.data)
        AppState.vaultKeeps = res.data
    }

    async deleteVault(id) {
        const res = await api.delete(`api/vaults/${id}`)
        logger.log(res.data)
        AppState.vaults = AppState.vaults.filter(v => v.id != id)
    }
    async deleteVaultKeep(id) {
        const res = await api.delete(`api/vaultkeeps/${id}`)
        logger.log(res.data)
        AppState.vaultKeeps = AppState.vaultKeeps.filter(vk => vk.id != id)
    }

}

export const vaultsService = new VaultsService()