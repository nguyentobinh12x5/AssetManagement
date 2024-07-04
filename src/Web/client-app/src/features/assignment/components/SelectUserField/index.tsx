import { FieldInputProps, useField } from "formik";
import React, { InputHTMLAttributes, useEffect, useState } from "react";
import { Col, Modal, Row } from "react-bootstrap";
import { Search } from "react-bootstrap-icons";
import ButtonIcon from "../../../../components/ButtonIcon";
import UserRadioSelectTable from "./UserRadioSelectTable";
import { Button } from "../../../../components";
import SearchBox from "../../../../components/SearchBox/SearchBox";
import useUserRadioSelect from "./useUserRadioSelect";

type ModalRadioSelectProps = InputHTMLAttributes<HTMLInputElement> & {
  label: string;
  placeholder?: string;
  name: string;
  required?: boolean;
  noValidation?: boolean;
  apiError?: string;
};
const SelectUserField: React.FC<ModalRadioSelectProps> = (props) => {
  const [field, { error, touched }, { setValue }] = useField<{
    id: string;
    fullName: string;
  }>(props);
  const [showModal, setShowModal] = useState(false);
  const { label, required, noValidation, apiError } = props;

  const validateClass = () => {
    if (touched && (error || error === "" || apiError)) return "is-invalid";
    if (noValidation) return "";
    if (touched) return "is-valid";
    return "";
  };

  const handleShowModal = () => {
    setShowModal(true);
  };

  const handleSelect = (value: any) => {
    setValue({
      fullName: value.fullName,
      id: value.id,
    });
  };

  return (
    <>
      <div className="form-group row">
        <label htmlFor={props.name} className="col-form-label col-4 d-flex">
          {label}
          {required && <div className="invalid ml-1">*</div>}
        </label>
        <div className="col">
          <div className="position-relative">
            <input
              className={`form-control ${validateClass()}`}
              {...props}
              name={field.name}
              value={field.value?.fullName}
              readOnly
            />
            <ButtonIcon
              className="form-btn-select__modal"
              onClick={handleShowModal}
            >
              <Search />
            </ButtonIcon>
          </div>

          {touched && (apiError || error) && (
            <div className="invalid position-relative mt-2">
              {apiError ? apiError : error}
            </div>
          )}
        </div>
      </div>
      <SelectModal
        isShow={showModal}
        closeDialog={() => setShowModal(false)}
        handleSelect={handleSelect}
      />
    </>
  );
};

export default SelectUserField;

interface SelectModalProps {
  isShow: boolean;
  dialogClassName?: string;
  closeDialog: () => void;
  handleSelect: (value: any) => void;
}
const SelectModal: React.FC<SelectModalProps> = ({
  isShow,
  dialogClassName,
  closeDialog,
  handleSelect,
}) => {
  const [value, setValue] = useState();
  const { handleSearch, searchTerm } = useUserRadioSelect();

  const handleSelectValue = () => {
    if (!value) return;

    handleSelect(value);
    closeDialog();
  };

  useEffect(() => {
    return () => {
      setValue(undefined);
    };
  }, []);

  return (
    <Modal
      show={isShow}
      dialogClassName={`radio-select-modal ${dialogClassName}`}
      aria-labelledby="select-user-modal"
      centered
    >
      <Modal.Header>
        <Modal.Title id="select-user-modal">
          <Row>
            <Col md={4}>Select User</Col>
            <Col md={{ span: 5, offset: 3 }}>
              <SearchBox
                defaultValue={searchTerm}
                handleFilterBySearchTerm={handleSearch}
              />
            </Col>
          </Row>
        </Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <UserRadioSelectTable
          selectedValue={value}
          handleSelect={(value) => setValue(value)}
        />
      </Modal.Body>
      <Modal.Footer>
        <div className="d-flex justify-content-end">
          <Button
            type="button"
            className="btn btn-danger me-4"
            onClick={handleSelectValue}
          >
            Save
          </Button>
          <Button
            type="button"
            className="btn btn-secondary"
            onClick={closeDialog}
          >
            Cancel
          </Button>
        </div>
      </Modal.Footer>
    </Modal>
  );
};
