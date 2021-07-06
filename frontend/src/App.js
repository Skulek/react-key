import "./App.css";
import { useEffect, useState } from "react";
import axios from "axios";
import useForm from "./lib/useForm";
import {
  Button,
  Container,
  Box,
  TextField,
  makeStyles,
} from "@material-ui/core";
import SaveIcon from "@material-ui/icons/Save";
import DeleteIcon from "@material-ui/icons/Delete";

const useStyles = makeStyles((theme) => ({
  button: {
    margin: theme.spacing(1),
    marginTop: "20px",
  },
  root: {
    "& > div": {
      margin: theme.spacing(1),
      width: "25ch",
    },
  },
  list: {
    margin: "auto",
    width: "300px",
  },
  li: {
    listStyle: "none",
  },
  baseIcon: {
    cursor: "pointer",
  },
}));

function App() {
  const classes = useStyles();
  const [keyValuePairs, setKeyValuePairs] = useState();

  const { inputs, handleChange, resetForm, clearForm } = useForm({
    key: "",
    value: "",
  });

  useEffect(() => {
    fetchData();
    return () => {};
  }, []);

  const fetchData = () => {
    axios
      .get("https://localhost:5001/keyvaluepair")
      .then((response) => {
        console.log(response.data);
        setKeyValuePairs(response.data);
      })
      .catch((error) => {
        // handle error
        console.log(error);
      });
  };

  const postData = (data, sucessMethod) => {
    axios
      .post("https://localhost:5001/keyvaluepair", data)
      .then((response) => {
        sucessMethod();
      })
      .catch((error) => {
        // handle error
      });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    postData(inputs, fetchData);
    clearForm();
  };

  return (
    <div className="App">
      <Container fixed>
        <Box className={classes.list}>
          <ul className={classes.li}>
            {!keyValuePairs && <p>loading...</p>}
            {keyValuePairs?.map((keyValuePair) => {
              return (
                <li key={keyValuePair.id}>
                  <span>
                    {keyValuePair.key} - {keyValuePair.value}
                  </span>
                  <DeleteIcon
                    className={classes.baseIcon}
                    onClick={() => {
                      console.log(keyValuePair.value);
                    }}
                  />
                </li>
              );
            })}
          </ul>
        </Box>
        <form className={classes.root} onSubmit={handleSubmit}>
          <TextField
            label="Key"
            type="text"
            name="key"
            id="key"
            placeholder="key"
            autoComplete="key"
            required
            value={inputs.key}
            onChange={(e) => handleChange(e)}
          />
          <TextField
            label="Value"
            type="text"
            name="value"
            id="value"
            placeholder="value"
            autoComplete="value"
            required
            value={inputs.value}
            onChange={(e) => handleChange(e)}
          />

          <Button
            type="submit"
            variant="contained"
            color="primary"
            className={classes.button}
            endIcon={<SaveIcon />}
          >
            Save
          </Button>
        </form>
      </Container>
    </div>
  );
}

export default App;
