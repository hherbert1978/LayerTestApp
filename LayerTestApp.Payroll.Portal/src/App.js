import './App.css';
import { store } from "./_actions/store";
import { Provider } from 'react-redux';
import PayGrades from './_components/PayGrades';
import { Container } from '@mui/material';

function App() {
  return (
    <Provider store={ store }>
      <Container maxWidth="lg">
        <PayGrades />
      </Container>      
    </Provider>
  );
}

export default App;
