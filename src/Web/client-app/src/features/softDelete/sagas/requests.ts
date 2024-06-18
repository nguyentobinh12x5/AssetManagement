import axios from 'axios';
import ENDPOINTS from '../../../constants/endpoints';

export function deleteUserRequest(userId: string) {
    return axios.delete(`${ENDPOINTS.DELETE_USER}/${userId}`);
}