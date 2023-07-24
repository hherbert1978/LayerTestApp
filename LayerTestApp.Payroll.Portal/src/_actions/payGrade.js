import api from "./api";

export const ACTION_TYPES = {
    GET_ALL: 'GET_ALL',
    CREATE: 'CREATE',
    UPDATE: 'UPDATE',
    DELETE: 'DELETE'
}

export const getAllPayGrades = () => dispatch => {
    api.payGrade().getAllPayGrades()
        .then(response => {
            // console.log(response)
            dispatch({
                type: ACTION_TYPES.GET_ALL,
                payload: response.data.result
            })
        })
        .catch(errorResponse => console.log(errorResponse))
}

export const createPayGrade = (data, onSuccess, onFailure) => dispatch => {   
    api.payGrade().createPayGrade(data)
        .then(response => {            
            dispatch({
                type: ACTION_TYPES.CREATE,
                payload: response.data.result
            })
            onSuccess("PayGrade successfully created!")
        })
        .catch(errorResponse => {
            // console.clear();
            // console.log(err.response);
            // console.log("\n");
            //var errors = failure.response.data.errors;
            // console.log(errors);
            // errors.push('Is Requiered.');
            // errors.push('Is not nice.');
            // err.response.data.errors.forEach(x => {
            //     console.log(x)
            // })
            //alert(errors.join("\n"));
            onFailure(errorResponse.response.data.errors, "PayGrade creation failure!");
        })
}

export const updatePayGrade = (data, onSuccess, onFailure) => dispatch => {
    console.log(data)
    api.payGrade().updatePayGrade(data)
        .then(response => {
            // console.log(response.data.result);
            dispatch({
                type: ACTION_TYPES.UPDATE,
                payload: response.data.result
            })
            onSuccess("PayGrade successfully updated!")
        })
        .catch(errorResponse => {
            onFailure(errorResponse.response.data.errors, "PayGrade update failure!");
        })
}

export const deletePayGrade = (data, onSuccess, onFailure) => dispatch => {
    const deletePayGradeDTO = { "payGradeId" : data }
    api.payGrade().deletePayGrade(deletePayGradeDTO)
        .then(response => {
            dispatch({
                type: ACTION_TYPES.DELETE,
                payload: response.data.result
            })
            onSuccess("PayGrade with PayGradeId='" + response.data.result.payGradeId.toString() + "' successfully deleted!")
        })
        .catch(errorResponse => {
            onFailure(errorResponse.response.data.errors, "PayGrade delete failure!");
        })
}