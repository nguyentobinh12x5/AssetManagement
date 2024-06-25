import React, { useState, useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import ConfirmModal from "../../../components/confirmModal/ConfirmModal";
import ButtonIcon from "../../../components/ButtonIcon";
import { XCircle } from "react-bootstrap-icons";
import { getAssetById } from '../reducers/asset-detail-slice';
import Table from "react-bootstrap/Table";
import { RootState } from "../../../redux/store";
import { Col, Row } from "react-bootstrap";
import { text } from "stream/consumers";

type AssetID = {
  id: string;
  onClose: () => void;
};

const DetailForm: React.FC<AssetID>  = ({id, onClose}) => {
  console.log('ID', id)
  const [isModalOpen, setIsModalOpen] = useState(true);
  const dispatch = useDispatch();
  const { assetDetail } = useSelector((state: RootState) => state.assetDetail);

  useEffect(() => {
    if (isModalOpen) {
      dispatch(getAssetById(id));
    }
    console.log(assetDetail)

  }, [isModalOpen, dispatch, id]);

  const handleCloseModal = () => {
    setIsModalOpen(false);
    onClose();
  };


  return (
    <div className="container m-auto p-5">
      <ConfirmModal
        title="Detailed Asset Information"
        isShow={isModalOpen}
        onHide={handleCloseModal}
        dialogClassName="modal-150w modal-detail"
        isShowClose={true}
      >
        <div className="form-detail">
          <div>
            <Row className="mb-3">
              <Col md={3}>
                Asset Code
              </Col>
              <Col md={9}>
                {assetDetail?.code}
              </Col>
            </Row>
          </div>
          <div>
            <Row className="mb-3">
              <Col md={3}>
                Asset Name
              </Col>
              <Col md={9}>
                {assetDetail?.name}
              </Col>
            </Row>
          </div>
          <div>
            <Row className="mb-3">
              <Col md={3}>
                Category
              </Col>
              <Col md={9}>
                {assetDetail?.categoryName}
              </Col>
            </Row>
          </div>
          <div>
            <Row className="mb-3">
              <Col md={3}>
                Installed Date
              </Col>
              <Col md={9}>
                {new Date(assetDetail?.installedDate ?? new Date()).toDateString()}
              </Col>
            </Row>
          </div>
          <div>
            <Row className="mb-3">
              <Col md={3}>
                State
              </Col>
              <Col md={9}>
                {assetDetail?.assetStatusName}
              </Col>
            </Row>
          </div>
          <div>
            <Row className="mb-3">
              <Col md={3}>
                Location
              </Col>
              <Col md={9}>
                {assetDetail?.location}
              </Col>
            </Row>
          </div>
          <div>
            <Row className="mb-3">
              <Col md={3}>
                Specification
              </Col>
              <Col md={5}>
                {assetDetail?.specification}
              </Col>
            </Row>
          </div>
          <div>
            <Row className="mb-3">
              <Col md={3} style={{paddingTop: 8
                
              }}>
                History
              </Col>
              <Col md={9}>
                <Table>
                  <thead>
                    <tr className="table-detail">
                      <th>Date</th>
                      <th>Assigned to</th>
                      <th>Assigned by</th>
                      <th>Returned Date</th>
                    </tr>
                  </thead>
                  <tbody>
                    <tr className="table-row-detail">
                      <td>12/10/2018</td>
                      <td>hungtv1</td>
                      <td>binhnv</td>
                      <td>07/03/2019</td>
                    </tr>
                    <tr className="table-row-detail">
                      <td>10/03/2024</td>
                      <td>thinhptx</td>
                      <td>tuanha</td>
                      <td>22/03/2024</td>
                    </tr>
                  </tbody>
                </Table>
              </Col>
            </Row>
          </div>
        </div>
      </ConfirmModal>
    </div>
  );
};

export default DetailForm;
