import { ElMessage } from 'element-plus';


const errorInterceptor = (error) => {
    const errorMessage = error.response?.data?.message || 'An error occurred';
    ElMessage.error(errorMessage); // Display error message using Element Plus
    return Promise.reject(error);

};

export default errorInterceptor;