import { ACTION_TYPES } from '../_actions/payGrade';
const initialState = {
    list: []
}

export const payGrade = (state = initialState, action) => {
    
    switch (action.type) {

        case ACTION_TYPES.GET_ALL:
            return {
                ...state,
                list: [...action.payload]
            }
        
        case ACTION_TYPES.CREATE:
            return {
                ...state,
                list: [...state.list, action.payload]
            }

        case ACTION_TYPES.UPDATE:
            return {
                ...state,
                list: state.list.map(x => x.payGradeId === action.payload.payGradeId ? action.payload : x)
            }

        case ACTION_TYPES.DELETE:
                return {
                    ...state,
                    list: state.list.filter(x => x.payGradeId !== action.payload.payGradeId)
                }

        default:
            return state;
    }
}