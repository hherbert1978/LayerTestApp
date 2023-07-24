import axios from "axios";

const baseURL = "http://localhost:5229/api/"

const api = {
    payGrade(url = baseURL + "PayGrade/") {
        return {
            getAllPayGrades: () => axios.get(url + 'GetAll'),
            createPayGrade: createPayGradeDTO => axios.post(url + 'Create', createPayGradeDTO),
            updatePayGrade: updatePayGradeDTO => axios.put(url + 'Update', updatePayGradeDTO),
            deletePayGrade: deletePayGradeDTO => axios.delete(url + 'Delete', 
                                                              { data: deletePayGradeDTO }, 
                                                              { headers: { "Content-Type": "application/json" } })
        }
    }
}

export default api;